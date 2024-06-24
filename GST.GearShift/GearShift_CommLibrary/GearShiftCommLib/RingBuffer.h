#include <iostream>
#include <iomanip>

#pragma once

//------------------------------------------------------------------------------------------------------
// As a result of VC++ limitation, all members functions in template classes must be defined in one file

namespace RingBfr
{
  template<class Type>
  class RingBuffer
  {
  private: static const long mBuffSize = 1024;
  private: Type* mData;
  private: long mWritePos;
  private: long mReadPos;
  private: long mBuffFill;

  public: RingBuffer()
          {
            mData = new Type[ mBuffSize ];
            mWritePos = 0;
            mReadPos = 0;
            mBuffFill = 0;
          }

  public: void Clear(void)
          {
            mWritePos = 0;
            mReadPos = 0;
            mBuffFill = 0;
          }

  public: bool PutData(Type data)
          {
            if (mBuffFill >= mBuffSize)
              return false;
            else
            {
              mData[mWritePos] = data;
              //std::cout << " WD " ;//<< mData[mWritePos] << endl;// " " << data << " " ;
              mWritePos++;
              if (mWritePos >= mBuffSize)
                mWritePos = 0;
              mBuffFill++;
              return true;
            }
          }

  public: bool GetData(Type *data)
          {
            if (mBuffFill <= 0)
              return false;
            else
            {
              //std::cout << " GD " ;//<< mData[mReadPos] << " ";
              *data = mData[mReadPos];
              mReadPos++;
              if (mReadPos >= mBuffSize)
                mReadPos = 0;
              mBuffFill--;
              return true;
            }
          }

  public: bool GetDataWithoutRemoval(Type *data)
          {
            if (mBuffFill <= 0)
              return false;
            else
            {
              *data = mData[mReadPos];
              return true;
            }
          }

  public: bool RemoveFirst()
          {
            if (mBuffFill <= 0)
              return false;
            else
            {
              mReadPos++;
              if (mReadPos >= mBuffSize)
                mReadPos = 0;
              mBuffFill--;
              return true;
            }
          }

  public: bool GetBfrDmp( Type *data )
          {
            int arrPos = 0;
            int tempReadPos = mReadPos;
            int tempBuffFill = mBuffFill;

            if (mBuffFill <= 0)
              return false;
            else
            {
              while (tempBuffFill)
              {
                data[arrPos] = mData[tempReadPos];
                arrPos++;
                tempReadPos++;
                if (tempReadPos >= mBuffSize)
                  tempReadPos = 0;
                tempBuffFill--;
              }
              return true;
            }
          }

  public: long GetBuffFill()
          {
              return mBuffFill;
          }
  };




}






