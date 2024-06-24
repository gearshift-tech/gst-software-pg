using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Soko.Common.Common
{
  public class FILECONVERTER_VspyToPcan
  {
    private static string[] _Header_PCAN = {

";$FILEVERSION=1.1",
";$STARTTIME=43044.4652323851",
";",
";   GENERATED.trc",
";",
";   Start time: 05-Nov-17 11:09:56.0780",
";   PCAN-Net: PCANLight_USB_16",
";",
";   Columns description:",
"; ~~~~~~~~~~~~~~~~~~~~~",
";   +-Message Number",
";   |          +Time offset(ms)",
";   |          |        +Type",
";   |          |        |        +ID(hex)",
";   |          |        |        |     +Data Length Code",
";   |          |        |        |     |   +Data bytes(hex)",
";   |          |        |        |     |   |",
";---+---   ----+----  --+--  ----+---  +  -+ -- -- -- -- -- -- --" };

    private class record
    {
      public UInt64 number = 0;
      public double timestamp = 0;
      public UInt32 ID = 0;
      public byte DLC = 0;
      public byte[] data = new byte[8];
    }


    private List<record> messages = new List<record>();
    private List<string> Rows = new List<string>();

    private string LeaveOnlyHex(string input)
    {
      string output = input;
      for (int i = 0; i < input.Length; i++)
      {
        if (input[i] == ' ')
        {
          output = input.Remove(i, input.Length - i);
          return output;
        }
      }
      return output;
    }

    public void LoadVspyFile(string src)
    {


      messages = new List<record>(); // Clear the old data
      Rows = new List<string>(); // Clear the old data

      using (var reader = new StreamReader(src)) // Read the whole file
      {

        while (!reader.EndOfStream)
        {
          Rows.Add(reader.ReadLine());
        }
      }

      Rows.RemoveRange(0, 62); // remove the initial vehicle spy header

      double timeOffset = Convert.ToDouble(Rows[0].Split(',')[1]) * 1000; // Assign time offset based on the first message timestamp

      foreach (string line in Rows) // Convert read lines into CAN messages
      {
        string[] split = line.Split(',');
        record msg = new record();

        msg.number = Convert.ToUInt64(split[0]);
        msg.timestamp = Convert.ToDouble(split[1]) * 1000 - timeOffset;

        string tmp = LeaveOnlyHex(split[9]);

        msg.ID = UInt32.Parse(LeaveOnlyHex( split[9]), System.Globalization.NumberStyles.HexNumber);

        int dataoffs = 12;
        for (int i = 0; i < 9; i++)
        {
          if (i == 8)
          {
            msg.DLC = 8;
            break;
          }
          if (split[dataoffs + i] == "")
          {
            msg.DLC = (byte)i;
            break;
          }
          else
          {
            msg.data[i] = byte.Parse(split[dataoffs + i], System.Globalization.NumberStyles.HexNumber);
          }
        }

        messages.Add(msg);

      }
    }

    public void GetCVTDiagnosticsStream()
    {
      List<byte[]> diagFrames = new List<byte[]>();
      
      byte[] diagFrame = new byte[200];
      bool diagFrameStarted = false;

      List<double[]> diagFramesDb = new List<double[]>();
      List<double> diagFramesTimestamps = new List<double>();

      double timestampTmp = 0;

      for (int idx = 0; idx < messages.Count; idx++)
      {
        record msg = messages[idx];
        if (msg.ID == 0x7E9 && msg.data[0] >= 0x21 && msg.data[0] <= 0x2F)
        {
          if (msg.data[0] == 0x21)
          {
            if (diagFrameStarted)
            {
              Console.WriteLine("INCOMPLETE FRAME!! " + idx+1.ToString());
              
            }
            else
            {
              Console.WriteLine("NEW FIRST FRAME " + idx+1.ToString());
              timestampTmp = msg.timestamp;
            }
            
            diagFrame = new byte[200];
            diagFrameStarted = true;
          }

          int arrayOffset = msg.data[0] - 0x21;
          arrayOffset *= 7; // there is 7 values per message
          //Console.WriteLine(arrayOffset.ToString());

        for (int i = 0; i < 7; i++)
          {
            diagFrame[arrayOffset + i] = msg.data[i + 1];
          }

        if (msg.data[0] == 0x2F)
          {
            Console.WriteLine("FULL FRAME READ " + idx+1.ToString());
            diagFrames.Add(diagFrame);
            diagFramesTimestamps.Add(timestampTmp);
            diagFrameStarted = false;
          }
        }
      }

      foreach (byte[] frm in diagFrames) // try to get the values out
      {
        List<double> fv = new List<double>();
        // BULK 1
        fv.Add( frm[0] * 158.0 / 255.0); //Estm VSP
        fv.Add(frm[1] * 8160.0 / 255); // Primary speed sensor
        fv.Add(frm[2] * 16320.0 / 255); // secondary speed sensor
        fv.Add(frm[3] * 16320.0 / 255); // vehicle speed sensor
        fv.Add(frm[4] * 8160.0 / 255); // engine speed
        fv.Add(frm[5] * 4.98 / 255); // line pressure volts
        fv.Add(frm[6] * 2.49 / 255); // ATF sensor volts

        // BULK 2
        fv.Add(frm[7] * 4.98 / 255.0);
        fv.Add(frm[8] * 19.92 / 255.0);
        fv.Add(frm[9] * 316.0 / 255.0);
        //fv[10] = frm[10] * .0 / 255.0); // NO DATA HERE
        fv.Add(frm[11] * 8160.0 / 255.0);
        fv.Add(frm[12] * 8160.0 / 255.0);
        fv.Add(frm[13] * 16320.0 / 255.0);

        // BULK 3
        fv.Add(frm[14] * 16320.0 / 255.0); // output speed
        fv.Add(frm[15] * 8160.0 / 255.0); // engine speed
        fv.Add(((sbyte)frm[16]) * 1200.0 / 255.0); // slip speed             // fix
        fv.Add(frm[17] * 10.2 / 255.0); // total gear ratio
        fv.Add(frm[18] * 10.2 / 255.0); // pulley gear ratio
        fv.Add(frm[19] * 255.0 / 255.0); // aux gearbox           // fix
        fv.Add(frm[20] * 7.9 / 255.0); // Gspeed


        //// BULK 4
        fv.Add(frm[21] * 81.0 / 255.0); // Accelerator position
        //fv[] = frm[] * .0 / 255.0); //
        //fv[] = frm[] * .0 / 255.0); //
        //fv[] = frm[] * .0 / 255.0); //
        //fv[] = frm[] * .0 / 255.0); //
        //fv[] = frm[] * .0 / 255.0); //
        fv.Add(frm[27] * 1632.0 / 255.0); // Engine torque

        // BULK 5
        //fv[] = frm[] * .0 / 255.0); //
        fv.Add(frm[29] * 3.9231 / 255.0); // torque RTO
        fv.Add(frm[30] * 6.63 / 255.0); // Line pressure
        fv.Add((frm[31] * 1.8)  - 40); // Fluid temp
        fv.Add(frm[32] * 8415.0 / 255.0); // DSR Revolution
        fv.Add(frm[33] * 10.2 / 255.0); // Target gear ratio
        fv.Add(frm[34] * 10.2 / 255.0); // target pulley gear ratio

        // BULK 6
        //fv[35] = frm[35] * .0 / 255.0); //
        fv.Add(frm[36] * 924.6 / 255.0); // LU pressure                // fix signed
        fv.Add(frm[37] * 924.6 / 255.0); // Line pressure
        fv.Add(frm[38] * 924.6 / 255.0); // target trim pressure
        fv.Add(frm[39] * 924.6 / 255.0); // target HC/RB
        fv.Add(frm[40] * 924.6 / 255.0); // target LB
        //fv[41] = frm[41] * .0 / 255.0); //

        // BULK 7
        fv.Add((frm[44] * 256 + frm[45]) / 1000.0); // ISOLT1
        fv.Add((frm[46] * 256 + frm[47]) / 1000.0); // ISOLT2
        fv.Add((frm[48] * 256 + frm[49]) / 1000.0); // PRI sol

        // BULK 8
        fv.Add((frm[50] * 256 + frm[51]) / 1000.0); // HC/RB
        fv.Add((frm[52] * 256 + frm[53]) / 1000.0); // L/B
        fv.Add((frm[54] * 256 + frm[55]) / 1000.0); // SOLMON

        // BULK 9
        fv.Add((frm[56] * 256 + frm[57]) / 1000.0); // PRI solmon
        fv.Add((frm[60] * 256 + frm[61]) / 1000.0); // HC/RB solmon
        fv.Add((frm[62] * 256 + frm[63]) / 1000.0); // HC/RB solmon



        diagFramesDb.Add(fv.ToArray());
      }


      using (var writer = new StreamWriter("D:/dstDiag.csv")) // Save thescaled frames in csv format so they can be plotted
      {
        string hdr = "TIME [s],";
        foreach (string nm in diagParamNames)
        {
          hdr += nm + ",";
        }
        writer.WriteLine(hdr); // Write header row with column names

        for (int z = 0; z < diagFramesDb.Count; z++) // Write rest of data
        {
          double[] frm = diagFramesDb[z];
          string line = (diagFramesTimestamps[z]/1000).ToString("0.000") + ",";
          for (int i = 0; i < (frm.Count()); i++)
          {
            line += frm[i].ToString("0.00") + ",";
          }
          writer.WriteLine(line);
        }
        writer.Flush();
      }


      using (var writer = new StreamWriter("D://dstDiag.txt")) // Save the frames as raw unscaled bytes
      {


        foreach (byte[] frm in diagFrames)
        {
          string line = "FRAME ";
          for (int i = 0; i < 120; i++)
          {
            line += frm[i].ToString("X2") + " ";
          }
          writer.WriteLine(line);
        }
      }
    }


    public void ConvertFiles(string src, string dst, List<UInt32> messagesToRemove)
    {
      LoadVspyFile(src);
      GetCVTDiagnosticsStream();

      if (messagesToRemove == null)
        messagesToRemove = new List<uint>();

      // SAVE THIS SHIT NOW
      using (var writer = new StreamWriter(dst))
      {
        foreach (string hdrline in _Header_PCAN)
        {
          writer.WriteLine(hdrline);
        }

        int position = 1;
        foreach (record msg in messages)
        {
          if (!messagesToRemove.Contains(msg.ID))
          {
            string line = "";

            line += string.Format("{0,6}", position) + ")   ";
            line += msg.timestamp.ToString("0.0").PadLeft(9) + "  Rx         ";
            line += msg.ID.ToString("X4") + "  ";
            line += msg.DLC.ToString() + "  ";

            for (int i = 0; i < 8; i++)
            {
              line += msg.data[i].ToString("X2") + " ";
            }


            writer.WriteLine(line);
            position++;
          }

        }
      }


      int x = 0;
      int y = x;
    }


    private string[] diagParamNames = new string[]
    {
      "ESTM VSP",
      "Primary spd sensor",
      "Secondary spd sensor",
      "VSS",
      "Engine SS",
      "Line pressure V",
      "ATF sensor V",
      "G sensor V", // Bullk2
      "VIGN",
      "VSS 2",
      "Input RPM",
      "Primary speed",
      "Secondary speed",
      "Output speed", // bulk3
      "Engine speed",
      "Slip speed",
      "Totral GR",
      "Pulley GR",
      "AUX gearbox",
      "Gspeed",
      "Accel pos", // Bulk 4
      "Eng Torque",
      "Torque rto", // Bulk 5
      "Line pressure",
      "Fluid temp",
      "DSR Revolution",
      "Target GR",
      "Target Pulley GR",
      "LU pressure", // Bulk 6
      "Line press",
      "Target trim press",
      "Target HC/RB",
      "Target LB",
      "ISOLT1 [A]", // Bulk 7
      "ISOLT2 [A]",
      "PRI sol [A]",
      "HC/RB sol [A]", // Bulk 8
      "L/B sol [A]",
      "Solmon [A]",
      "PRI Solmon [A]",
      "HC/RB Solmon [A]",
      "L/B Solmon [A]",
    };

  }
}
