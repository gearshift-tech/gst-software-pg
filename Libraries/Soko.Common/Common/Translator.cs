// Translator.cs

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.IO;
using System.Xml;


namespace Soko.Common.Common
{
    /// <summary>
    /// Tramslates phrases into other languages.
    /// </summary>
    /// <remarks>
    /// Class:              Translator
    /// Author:             Pawel Pustelnik
    /// Created:            20.03.2007
    /// Last modification:  18.12.2007
    /// 
    /// Description:
    /// Translator is responsible for translating texts. 
    /// It loads dictionary from UTF-8 XML file and then is able to translate given phrase.
    /// </remarks>
    public class Translator
    {
        #region Fields
        
        /// <summary>
        /// Dictionaty table
        /// </summary>
        private static Hashtable words;

        /// <summary>
        /// List of missed words. These are the words which have 
        /// no translation in dictionary file and are stored during
        /// debug mode in application. They are used later to fill
        /// dictionary with them.
        /// </summary>
        private static List<string> missedWords;
        
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public Translator()
        {
            Init();
        }


        /// <summary>
        /// Translates given text
        /// </summary>
        /// <param name="toTranslate">Text to translate</param>
        /// <returns>Traslated text</returns>
        public static string Tr(string toTranslate)
        {
            if (toTranslate == null)
                return "";

            if (words == null)
                return toTranslate;

            string translated = null;

            if (words.ContainsKey(toTranslate))
            {
                translated = (string)words[toTranslate];
            }
            else
            {
                #if DEBUG
                if (!missedWords.Contains(toTranslate))
                {
                    missedWords.Add(toTranslate);
                }
                #endif
            }
            
            if (translated == null)
            {
                translated = toTranslate;
            }

            return translated;
        }

        /// <summary>
        /// Translate given text back
        /// </summary>
        /// <param name="toTranslateBack">Text to translate back</param>
        /// <returns>Translated (back) text</returns>
        public static string InvTr(string toTranslateBack)
        {
            if (toTranslateBack == null)
                return "";

            if (words == null)
                return toTranslateBack;

            string translated = null;

            if (words.ContainsValue(toTranslateBack))
            {
                foreach (string key in words.Keys)
                {
                    if ((string)words[key] == toTranslateBack)
                    {
                        translated = key;
                        break;
                    }
                }
            }

            if (translated == null)
            {
                translated = toTranslateBack;
            }

            return translated;
        }

        /// <summary>
        /// Reads language name from translation file header
        /// </summary>
        /// <param name="langFileName">Language file name</param>
        /// <returns>Language name</returns>
        public static string GetLanguageName(string langFileName)
        {
            Stream stream = new FileStream(langFileName, FileMode.Open, FileAccess.Read);
            Encoding enc = System.Text.Encoding.UTF8;

            using (StreamReader reader = new StreamReader(stream, enc))
            {
                string input = reader.ReadToEnd();

                reader.Dispose();
                stream.Dispose();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(input);

                if (xmlDoc == null)
                    return null;

                 XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("language");
                 if (root != null)
                 {
                     return root.GetAttribute("name");
                 }
                 else
                     return null;
            }

        }
      
        /// <summary>
        /// Initializes translator - must be called once before staring work with Translator.tr() [for every language change]
        /// </summary>
        /// <param name="stream">Stream Data</param>
        /// <param name="enc">Used encoding</param>
        public static void InitTranslator(Stream stream, Encoding enc)
        {

            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            using (StreamReader reader = new StreamReader(stream, enc))
            {
                string input = reader.ReadToEnd();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(input);

                if (xmlDoc == null)
                    return;

                if (words == null)
                {
                    words = new Hashtable();
                }
                else
                {
                    words.Clear();
                }

                XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("language");
                if (root != null)
                {
                    XmlNodeList phrases = root.SelectNodes("phrase");
                    foreach (XmlElement phrase in phrases)
                    {
                        string key = phrase.GetAttribute("name");
                        string translation = phrase.GetAttribute("translation");

                        if (!words.Contains(key))
                            words.Add(key, translation);
                    }
                }
                

                if (missedWords == null)
                {
                    missedWords = new List<string>();
                }

                reader.Close();
                reader.Dispose();
            }
        }

        /// <summary>
        /// Initialize dictionary
        /// </summary>
        private void Init()
        {
            words = new Hashtable();
            missedWords = new List<string>();
        }


        /// <summary>
        /// Stores missed words in text file. It is useful in DEBUG mode.
        /// </summary>
        public static void StoreMissedWords()
        {
            if (missedWords != null)
            {
                string fileName = "MISSED_WORDS.TXT";
                using (StreamWriter writer = File.CreateText(fileName))
                {
                    foreach (string text in missedWords)
                    {
                        string missedWord = text.Replace("\"", "&quot;");
                        missedWord = missedWord.Replace("<", "&lt;");
                        missedWord = missedWord.Replace(">", "&gt;");
                        missedWord = missedWord.Replace("'", "&#39;");
                        missedWord = missedWord.Replace("&", "&amp;");

                        writer.WriteLine("<phrase name=\"" + missedWord + "\" translation=\"" + missedWord + "\" />");
                    }
                }
            }
        }
    }

}