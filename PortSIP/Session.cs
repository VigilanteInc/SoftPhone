using System;
using System.Collections.Generic;
using System.Text;



namespace PortSIP
{
    class Session
    {
        private int mSessionId = 0;
        private bool mHoldState = false;
        private bool mSessionState = false;
        private bool mRecvCallState = false;
        private int mOriginCallSessionId = 0;
        private bool mIsReferCall = false;
        private bool mHasEarlyMedia = false;
        private int mDialogMessageId = 0;
        private int mOutOfDialogMessageId = 0;

        public bool hasEarlyMeida()
        {
            return mHasEarlyMedia;
        }
        public void setEarlyMeida(bool earlyMedia)
        {
            mHasEarlyMedia = earlyMedia;
        }

        public bool isReferCall()
        { 
            return mIsReferCall; 
        }

        public int getOriginCallSessionId() 
        { 
            return mOriginCallSessionId; 
        }

        public void setReferCall(bool referCall, int originCallSessionId)
        {
            mIsReferCall = referCall;
            mOriginCallSessionId = originCallSessionId;
        }

        public void setDialogMessageId(int id)
        {
            mDialogMessageId = id;
        }

        public int getDialogMessageId()
        {
            return mDialogMessageId;
        }

        public void setOutOfDialogMessageId(int id)
        {
            mOutOfDialogMessageId = id;
        }

        public int getOutOfDialogMessageId()
        {
            return mOutOfDialogMessageId;
        }



        public void reset()
        {
            mSessionId = 0;
            mHoldState = false;
            mSessionState = false;
            mRecvCallState = false;
            mOriginCallSessionId = 0;
            mIsReferCall = false;
            mDialogMessageId = 0;
            mOutOfDialogMessageId = 0;
        }


        public void setSessionId(int sessionId)
        {
            mSessionId = sessionId;
        }


        public int getSessionId()
        {
            return mSessionId;
        }

        public void setHoldState(bool state)
        {
            mHoldState = state;
        }


        public bool getHoldState()
        {
            return mHoldState;
        }

        public void setSessionState(bool state)
        {
            mSessionState = state;
        }


        public bool getSessionState()
        {
            return mSessionState;
        }



        public void setRecvCallState(bool state)
        {
            mRecvCallState = state;
        }


        public bool getRecvCallState()
        {
            return mRecvCallState;
        }

    }
}
