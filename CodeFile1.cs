using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using PortSIP;
using System.Xml.Serialization;

namespace SoftPhone
{


    /// <summary>
    ///  SoftPhone version 0.0.1a
    ///  Cypress Access Systems
    ///  Author: Vincent Abate 
    ///  copyright: 2016
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class SoftPhone : SIPCallbackEvents

    {
        public string appName = "KPhone";
        public string appVersion = "version 0.0.4a";
        public string appAuthor = "Vincent Abate";
        public string appCopyright = "Cypress Access Systems";


        private const int MAX_LINES = 9; // Maximum lines
        private const int LINE_BASE = 1;


        private Session[] _CallSessions = new Session[MAX_LINES];
        private int _CurrentlyLine = LINE_BASE;
        private bool _SIPInited = false;
        private bool _SIPLogined = false;
        


        private PortSIPLib _sdkLib;

      

        private int findSession(int sessionId)
        {
           
            int index = -1;
            try
            {
                for (int i = LINE_BASE; i < MAX_LINES; ++i)
                {
                    if (_CallSessions[i].getSessionId() == sessionId)
                    {
                        index = i;
                        break;
                    }
                }
            }
            catch (Exception) { }
            return index;
        }


        private byte[] GetBytes(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }

        private string GetString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }


        private string getLocalIP()
        {
            StringBuilder localIP = new StringBuilder();
            localIP.Length = 64;
            int nics = _sdkLib.getNICNums();
            for (int i = 0; i < nics; ++i)
            {
                _sdkLib.getLocalIpAddress(i, localIP, 64);
                if (localIP.ToString().IndexOf(":") == -1)
                {
                    // No ":" in the IP then it's the IPv4 address, we use it in our sample
                    break;
                }
                else
                {
                    // the ":" is occurs in the IP then this is the IPv6 address.
                    // In our sample we don't use the IPv6.
                }

            }

            return localIP.ToString();
        }


        private void updatePrackSetting()
        {
            if (!_SIPInited)
            {
                return;
            }

            _sdkLib.enableReliableProvisional(true);
        }

        private void loadDevices()
        {
            if (_SIPInited == false)
            {
                return;
            }

          

            _sdkLib.setAudioDeviceId(0, 0);

            int volume = _sdkLib.getSpeakerVolume();


        }


        private void InitSettings()
        {


            _sdkLib.setDoNotDisturb(false);
        }


        private void SetSRTPType()
        {
            if (_SIPInited == false)
            {
                return;
            }

            SRTP_POLICY SRTPPolicy = SRTP_POLICY.SRTP_POLICY_NONE;

            _sdkLib.setSrtpPolicy(SRTPPolicy);
        }

        // Default we just using PCMU, PCMA, and G.279
        private void InitDefaultAudioCodecs()
        {
            if (_SIPInited == false)
            {
                return;
            }


            _sdkLib.clearAudioCodec();


            // Default we just using PCMU, PCMA, G729
            _sdkLib.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
            _sdkLib.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            _sdkLib.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);

            _sdkLib.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_DTMF);  // for DTMF as RTP Event - RFC2833
        }

        public void deRegisterFromServer()
        {
            if (_SIPInited == false)
            {
                return;
            }
            try
            {
                for (int i = LINE_BASE; i < MAX_LINES; ++i)
                {

                    if (_CallSessions[i].getRecvCallState() == true)
                    {
                        _sdkLib.rejectCall(_CallSessions[i].getSessionId(), 486);
                    }
                    else if (_CallSessions[i].getSessionState() == true)
                    {
                        _sdkLib.hangUp(_CallSessions[i].getSessionId());
                    }

                    _CallSessions[i].reset();
                }
            }
            catch (Exception) {
                return;
            }
            if (_SIPLogined)
            {
                _sdkLib.unRegisterServer();
                _SIPLogined = false;
            }


            if (_SIPInited)
            {
                _sdkLib.unInitialize();
                _sdkLib.releaseCallbackHandlers();

                _SIPInited = false;
            }


            _CurrentlyLine = LINE_BASE;


        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///  With below all onXXX functions, you MUST use the Invoke/BeginInvoke method if you want
        ///  modify any control on the Forms.
        ///  More details please visit: http://msdn.microsoft.com/en-us/library/ms171728.aspx
        ///  The Invoke method is recommended.
        ///  
        ///  if you don't like Invoke/BeginInvoke method, then  you can add this line to Form_Load:
        ///  Control.CheckForIllegalCrossThreadCalls = false;
        ///  This requires .NET 2.0 or higher
        /// 
        /// </summary>
        /// 
        public Int32 onRegisterSuccess(Int32 callbackIndex, Int32 callbackObject, String statusText, Int32 statusCode)
        {
            // use the Invoke method to modify the control.
          
            //Console.WriteLine("Code: [" + statusCode + "] - Ready.\n", Console.ForegroundColor = ConsoleColor.Green);
            //Console.ForegroundColor = ConsoleColor.Gray;
            _SIPLogined = true;

            return 0;
        }


        public Int32 onRegisterFailure(Int32 callbackIndex, Int32 callbackObject, String statusText, Int32 statusCode)
        {
           ;
            Console.Write("Code: [" + statusCode + "] " + statusText, Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("-There was a problem registering with the SIP provider.\n");

            _SIPLogined = false;

            return 0;
        }


        public Int32 onInviteIncoming(Int32 callbackIndex,
                                             Int32 callbackObject,
                                             Int32 sessionId,
                                             String callerDisplayName,
                                             String caller,
                                             String calleeDisplayName,
                                             String callee,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            int index = -1;
            for (int i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionState() == false && _CallSessions[i].getRecvCallState() == false)
                {
                    index = i;
                    _CallSessions[i].setRecvCallState(true);
                    break;
                }
            }

            if (index == -1)
            {
                _sdkLib.rejectCall(sessionId, 486);
                return 0;
            }

            if (existsVideo)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }
            if (existsAudio)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }

            _CallSessions[index].setSessionId(sessionId);
            string Text = string.Empty;

            
            int j = 0;

            for (j = LINE_BASE; j < MAX_LINES; ++j)
            {
                if (_CallSessions[j].getSessionState() == true)
                {
                   
                    break;
                }
            }

           
            //  You should write your own code to play the wav file here for alert the incoming call(incoming tone);

            return 0;

        }

        public Int32 onInviteTrying(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Call is trying...";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onInviteSessionProgress(Int32 callbackIndex,
                                            Int32 callbackObject,
                                            Int32 sessionId,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsEarlyMedia,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            if (existsVideo)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }
            if (existsAudio)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }

            _CallSessions[i].setSessionState(true);

            string Text = "Line " + i.ToString();
            Text = Text + ": Call session progress.";


            Console.WriteLine(Text);


            _CallSessions[i].setEarlyMeida(existsEarlyMedia);

            return 0;
        }

        public Int32 onInviteRinging(Int32 callbackIndex,
                                            Int32 callbackObject,
                                            Int32 sessionId,
                                            String statusText,
                                            Int32 statusCode)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            if (_CallSessions[i].hasEarlyMeida() == false)
            {
                // No early media, you must play the local WAVE  file for ringing tone
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Ringing...";


            Console.WriteLine(Text);



            return 0;
        }


        public Int32 onInviteAnswered(Int32 callbackIndex,
                                             Int32 callbackObject,
                                             Int32 sessionId,
                                             String callerDisplayName,
                                             String caller,
                                             String calleeDisplayName,
                                             String callee,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            if (existsVideo)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }
            if (existsAudio)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }


            _CallSessions[i].setSessionState(true);

            string Text = "Line " + i.ToString();
            Text = Text + ": Call established";


            Console.WriteLine(Text);

            //joinConference(i);


            // If this is the refer call then need set it to normal
            if (_CallSessions[i].isReferCall())
            {
                _CallSessions[i].setReferCall(false, 0);
            }

            return 0;
        }


        public Int32 onInviteFailure(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            int index = findSession(sessionId);
            if (index == -1)
            {
                return 0;
            }

            string Text = "Line " + index.ToString();
            Text += ": call failure, ";
            Text += reason;
            Text += ", ";
            Text += code.ToString();


            Console.WriteLine(Text);



            if (_CallSessions[index].isReferCall())
            {
                // Take off the origin call from HOLD if the refer call is failure
                int originIndex = -1;
                for (int i = LINE_BASE; i < MAX_LINES; ++i)
                {
                    // Looking for the origin call
                    if (_CallSessions[i].getSessionId() == _CallSessions[index].getOriginCallSessionId())
                    {
                        originIndex = i;
                        break;
                    }
                }

                if (originIndex != -1)
                {
                    _sdkLib.unHold(_CallSessions[index].getOriginCallSessionId());
                    _CallSessions[originIndex].setHoldState(false);

                    // Switch the currently line to origin call line
                    _CurrentlyLine = originIndex;
                    // ComboBoxLines.SelectedIndex = _CurrentlyLine - 1;

                    Text = "Current line is set to: ";
                    Text += _CurrentlyLine.ToString();


                    Console.WriteLine(Text);


                }

                _CallSessions[index].reset();


            }
            return 0;
        }


        public Int32 onInviteUpdated(Int32 callbackIndex,
                                             Int32 callbackObject,
                                             Int32 sessionId,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            if (existsVideo)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }
            if (existsAudio)
            {
                // If more than one codecs using, then they are separated with "#",
                // for example: "g.729#GSM#AMR", "H264#H263", you have to parse them by yourself.
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Call is updated";


            Console.WriteLine(Text);


            return 0;
        }

        public Int32 onInviteConnected(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Call is connected";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onInviteBeginingForward(Int32 callbackIndex, Int32 callbackObject, String forwardTo)
        {
            string Text = "An incoming call was forwarded to: ";
            Text = Text + forwardTo;


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onInviteClosed(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {

            EndCall();
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }
            
            _CallSessions[i].reset();

            string Text = "Line " + i.ToString();
            Text = Text + ": Call closed";
            

            Console.WriteLine(Text);
            

            return 0;
        }


        public Int32 onRemoteHold(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Placed on hold by remote.";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onRemoteUnHold(Int32 callbackIndex,
                                             Int32 callbackObject,
                                             Int32 sessionId,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Take off hold by remote.";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onReceivedRefer(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    Int32 sessionId,
                                                    Int32 referId,
                                                    String to,
                                                    String from,
                                                    String referSipMessage)
        {


            int index = findSession(sessionId);
            if (index == -1)
            {
                _sdkLib.rejectRefer(referId);
                return 0;
            }


            string Text = "Received REFER on line ";
            Text += index.ToString();
            Text += ", refer to ";
            Text += to;


            Console.WriteLine(Text);


            // Accept the REFER automatically
            int referSessionId = _sdkLib.acceptRefer(referId, referSipMessage);
            if (referSessionId <= 0)
            {
                Text = "Failed to accept REFER on line ";
                Text += index.ToString();


                Console.WriteLine(Text);
            }

            else
            {
                _sdkLib.hangUp(_CallSessions[index].getSessionId());
                _CallSessions[index].reset();


                _CallSessions[index].setSessionId(referSessionId);
                _CallSessions[index].setSessionState(true);

                Text = "Accepted the REFER";

                Console.WriteLine(Text);




            }
            return 0;
        }


        public Int32 onReferAccepted(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int index = findSession(sessionId);
            if (index == -1)
            {
                return 0;
            }

            string Text = "Line ";
            Text += index.ToString();
            Text += ", the REFER was accepted";


            Console.WriteLine(Text);


            return 0;
        }



        public Int32 onReferRejected(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            int index = findSession(sessionId);
            if (index == -1)
            {
                return 0;
            }

            string Text = "Line ";
            Text += index.ToString();
            Text += ", the REFER was rejected";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onTransferTrying(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Transfer Trying";


            Console.WriteLine(Text);



            return 0;
        }

        public Int32 onTransferRinging(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Transfer Ringing";


            Console.WriteLine(Text);


            return 0;
        }



        public Int32 onACTVTransferSuccess(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            // Close the call after succeeded transfer the call
            _sdkLib.hangUp(_CallSessions[i].getSessionId());
            _CallSessions[i].reset();

            string Text = "Line " + i.ToString();
            Text = Text + ": Transfer succeeded, call closed.";


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onACTVTransferFailure(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Line " + i.ToString();
            Text = Text + ": Transfer failure";


            Console.WriteLine(Text);


            //  reason is error reason
            //  code is error code

            return 0;
        }

        public Int32 onReceivedSignaling(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            // This event will be fired when the SDK received a SIP message
            // you can use signaling to access the SIP message.

            return 0;
        }


        public Int32 onSendingSignaling(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            // This event will be fired when the SDK sent a SIP message
            // you can use signaling to access the SIP message.

            return 0;
        }




        public Int32 onWaitingVoiceMessage(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {

            string Text = messageAccount;
            Text += " has voice message.";



            Console.WriteLine(Text);


            // You can use these parameters to check the voice message count

            //  urgentNewMessageCount;
            //  urgentOldMessageCount;
            //  newMessageCount;
            //  oldMessageCount;

            return 0;
        }


        public Int32 onWaitingFaxMessage(Int32 callbackIndex,
                                                       Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            string Text = messageAccount;
            Text += " has FAX message.";



            Console.WriteLine(Text);



            // You can use these parameters to check the FAX message count

            //  urgentNewMessageCount;
            //  urgentOldMessageCount;
            //  newMessageCount;
            //  oldMessageCount;

            return 0;
        }


        public Int32 onRecvDtmfTone(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, Int32 tone)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string DTMFTone = tone.ToString();
            switch (tone)
            {
                case 10:
                    DTMFTone = "*";
                    break;

                case 11:
                    DTMFTone = "#";
                    break;

                case 12:
                    DTMFTone = "A";
                    break;

                case 13:
                    DTMFTone = "B";
                    break;

                case 14:
                    DTMFTone = "C";
                    break;

                case 15:
                    DTMFTone = "D";
                    break;

                case 16:
                    DTMFTone = "FLASH";
                    break;
            }

            string Text = "Received DTMF Tone: ";
            Text += DTMFTone;
            Text += " on line ";
            Text += i.ToString();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Text);
            Console.ForegroundColor = ConsoleColor.Gray;


            return 0;
        }


        public Int32 onPresenceRecvSubscribe(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String fromDisplayName,
                                                    String from,
                                                    String subject)
        {


            return 0;
        }


        public Int32 onPresenceOnline(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    String fromDisplayName,
                                                    String from,
                                                    String stateText)
        {

            return 0;
        }

        public Int32 onPresenceOffline(Int32 callbackIndex, Int32 callbackObject, String fromDisplayName, String from)
        {


            return 0;
        }


        public Int32 onRecvOptions(Int32 callbackIndex, Int32 callbackObject, StringBuilder optionsMessage)
        {
            //         string text = "Received an OPTIONS message: ";
            //       text += optionsMessage.ToString();
            //     MessageBox.Show(text, "Received an OPTIONS message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return 0;
        }

        public Int32 onRecvInfo(Int32 callbackIndex, Int32 callbackObject, StringBuilder infoMessage)
        {
            string text = "Received a INFO message: ";
            text += infoMessage.ToString();

            Console.WriteLine(text, "Received a INFO message");

            return 0;
        }


        public Int32 onRecvMessage(Int32 callbackIndex,
                                                 Int32 callbackObject,
                                                 Int32 sessionId,
                                                 String mimeType,
                                                 String subMimeType,
                                                 byte[] messageData,
                                                 Int32 messageDataLength)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string text = "Received a MESSAGE message on line ";
            text += i.ToString();

            if (mimeType == "text" && subMimeType == "plain")
            {
                string mesageText = GetString(messageData);
            }
            else if (mimeType == "application" && subMimeType == "vnd.3gpp.sms")
            {
                // The messageData is binary data
            }
            else if (mimeType == "application" && subMimeType == "vnd.3gpp2.sms")
            {
                // The messageData is binary data
            }

            Console.WriteLine(text, "Received a MESSAGE message");

            return 0;
        }


        public Int32 onRecvOutOfDialogMessage(Int32 callbackIndex,
                                                 Int32 callbackObject,
                                                 String fromDisplayName,
                                                 String from,
                                                 String toDisplayName,
                                                 String to,
                                                 String mimeType,
                                                 String subMimeType,
                                                 byte[] messageData,
                                                 Int32 messageDataLength)
        {
            string text = "Received a message(out of dialog) from ";
            text += from;

            if (mimeType == "text" && subMimeType == "plain")
            {
                string mesageText = GetString(messageData);
                int i = 0;
            }
            else if (mimeType == "application" && subMimeType == "vnd.3gpp.sms")
            {
                // The messageData is binary data
            }
            else if (mimeType == "application" && subMimeType == "vnd.3gpp2.sms")
            {
                // The messageData is binary data
            }

            Console.WriteLine(text, "Received a out of dialog MESSAGE message");

            return 0;
        }

        public Int32 onSendMessageSuccess(Int32 callbackIndex,
                                                           Int32 callbackObject,
                                                           Int32 sessionId, Int32 messageId)
        {
            return 0;
        }


        public Int32 onSendMessageFailure(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 sessionId,
                                                        Int32 messageId,
                                                        String reason,
                                                        Int32 code)
        {

            return 0;
        }



        public Int32 onSendOutOfDialogMessageSuccess(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 messageId,
                                                        String fromDisplayName,
                                                        String from,
                                                        String toDisplayName,
                                                        String to)
        {


            return 0;
        }

        public Int32 onSendOutOfDialogMessageFailure(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 messageId,
                                                        String fromDisplayName,
                                                        String from,
                                                        String toDisplayName,
                                                        String to,
                                                        String reason,
                                                        Int32 code)
        {
            return 0;
        }


        public Int32 onPlayAudioFileFinished(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String fileName)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Play audio file - ";
            Text += fileName;
            Text += " end on line: ";
            Text += i.ToString();


            Console.WriteLine(Text);


            return 0;
        }

        public Int32 onPlayVideoFileFinished(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            int i = findSession(sessionId);
            if (i == -1)
            {
                return 0;
            }

            string Text = "Play video file end on line: ";
            Text += i.ToString();


            Console.WriteLine(Text);


            return 0;
        }


        public Int32 onReceivedRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize)
        {
            /*
                !!! IMPORTANT !!!

                Don't call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */

            return 0;
        }

        public Int32 onSendingRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize)
        {

            /*
                !!! IMPORTANT !!!

                Don't call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */
            return 0;
        }


        public Int32 onAudioRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] data,
                                               Int32 dataLength,
                                               Int32 samplingFreqHz)
        {

            /*
                !!! IMPORTANT !!!

                Don't call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */

            // The data parameter is audio stream as PCM format, 16bit, Mono.
            // the dataLength parameter is audio steam data length.



            //
            // IMPORTANT: the data length is stored in dataLength parameter!!!
            //

            AUDIOSTREAM_CALLBACK_MODE type = (AUDIOSTREAM_CALLBACK_MODE)callbackType;

            if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_LOCAL_MIX)
            {
                // The callback data is mixed from local record device - microphone
                // The sessionId is CALLBACK_SESSION_ID.PORTSIP_LOCAL_MIX_ID

            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_REMOTE_MIX)
            {
                // The callback data is mixed from local record device - microphone
                // The sessionId is CALLBACK_SESSION_ID.PORTSIP_REMOTE_MIX_ID
            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_LOCAL_PER_CHANNEL)
            {
                // The callback data is from local record device of each session, use the sessionId to identifying the session.
            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_REMOTE_PER_CHANNEL)
            {
                // The callback data is received from remote side of each session, use the sessionId to identifying the session.
            }




            return 0;
        }


        public Int32 onVideoRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               Int32 width,
                                               Int32 height,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] data,
                                               Int32 dataLength)
        {
            /*
                !!! IMPORTANT !!!

                Don't call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

                The video data format is YUV420, YV12.
            */

            //
            // IMPORTANT: the data length is stored in dataLength parameter!!!
            //

            VIDEOSTREAM_CALLBACK_MODE type = (VIDEOSTREAM_CALLBACK_MODE)callbackType;

            if (type == VIDEOSTREAM_CALLBACK_MODE.VIDEOSTREAM_LOCAL)
            {

            }
            else if (type == VIDEOSTREAM_CALLBACK_MODE.VIDEOSTREAM_REMOTE)
            {

            }


            return 0;

        }
        public void EndCall() {
            

            if (_CallSessions[_CurrentlyLine].getRecvCallState() == true)
            {
                _sdkLib.rejectCall(_CallSessions[_CurrentlyLine].getSessionId(), 486);
                _CallSessions[_CurrentlyLine].reset();

                string Text = "Line " + _CurrentlyLine.ToString();
                Text = Text + ": Rejected call";
                Console.WriteLine(Text);
                SIPSample.Program.isActive = false;
                deRegisterFromServer();
                return;
            }

            if (_CallSessions[_CurrentlyLine].getSessionState() == true)
            {
                _sdkLib.hangUp(_CallSessions[_CurrentlyLine].getSessionId());
                _CallSessions[_CurrentlyLine].reset();

                string Text = "Line " + _CurrentlyLine.ToString();
                Text = Text + ": Call ended.";
                Console.WriteLine(Text);
                SIPSample.Program.isActive = false;
                deRegisterFromServer();
                return;
            }
            
        }


        public void makeCall(string phone) {
            string callTo = phone;
            _CallSessions = new Session[MAX_LINES];
           _CurrentlyLine = 1;
           // UpdateAudioCodecs();

            // Ensure the we have been added one audio codec at least
            if (_sdkLib.isAudioCodecEmpty() == true)
            {
                InitDefaultAudioCodecs();
            }

           
            _sdkLib.setAudioDeviceId(0, 0);

            int sessionId = _sdkLib.call(callTo, false, false);
            if (sessionId <= 0)
            {
                Console.WriteLine("Call failure");
                return;
            }
            try
            {
                if (_CallSessions[_CurrentlyLine].getSessionState() == true || _CallSessions[_CurrentlyLine].getRecvCallState() == true)
                {
                    Console.WriteLine("Current line is busy now, please switch a line.");
                    return;
                } 
               

            }
            catch (Exception)
            {
                _CallSessions[_CurrentlyLine] = new Session();
                _CallSessions[_CurrentlyLine].setSessionId(sessionId);
                _CallSessions[_CurrentlyLine].setSessionState(true);
                //return;

            }
            string Text = "Line " + _CurrentlyLine.ToString();
            Text = Text + ": Calling...";
            Console.WriteLine(Text);
        }

        [Serializable()]
        public class PhoneConfig
        {
            private string username;
            public string UserName
            {
                get { return username; }
                set { username = value; }
            }
            private string password;
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            private string sipserver;
            public string SIPServer
            {
                get { return sipserver; }
                set { sipserver = value; }
            }
            private int sipport;
            public int SIPPort {
                get { return sipport; }
                set { sipport = value; }
            }
           

        }

        public void StartPhone(string[] args )
        {
            if (_SIPInited == true)
            {
                Console.WriteLine("You are already logged in.");
                return;
            }

            //
            //this is pulling the SIP provider configuration from config.xml
            //
            try
            {
                XmlSerializer SerializerObj = new XmlSerializer(typeof(PhoneConfig));
                FileStream phoneCfg = new FileStream(Directory.GetCurrentDirectory() + @"\config.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                PhoneConfig thisPhone = (PhoneConfig)SerializerObj.Deserialize(phoneCfg);
                phoneCfg.Close();


                string userName = thisPhone.UserName; //YOU MUST SET THIS IN CONFIG.XML
                string password = thisPhone.Password; //YOU MUST SET THIS IN CONFIG.XML
                string SIPServer = thisPhone.SIPServer; //YOU MUST SET THIS IN CONFIG.XML
                int SIPServerPort = thisPhone.SIPPort; //YOU MUST SET THIS IN CONFIG.XML
                string userDomain = "";
                string displayName = "";
                string authName = "";
                int outboundServerPort = 0;
                string outboundServer = "";
                string stunServer = "";
                int StunServerPort = 0;

                TRANSPORT_TYPE transport = TRANSPORT_TYPE.TRANSPORT_UDP;

                _sdkLib = new PortSIPLib(0, 0, this);

                //
                // Create and set the SIP callback handers, this MUST called before
                // _sdkLib.initialize();
                //
                _sdkLib.createCallbackHandlers();

                string logFilePath = "d:\\"; // The log file path, you can change it - the folder MUST exists
                string agent = "PortSIP VoIP SDK 11.2";


                // Initialize the SDK
                int rt = _sdkLib.initialize(transport,
                                 PORTSIP_LOG_LEVEL.PORTSIP_LOG_NONE,
                                 logFilePath,
                                 MAX_LINES,
                                 agent,
                                 0,
                                 0);

                if (rt != 0)
                {
                    _sdkLib.releaseCallbackHandlers();
                    Console.WriteLine("Initialization failure.");
                    return;
                }

                Console.WriteLine(this.appName + "-" + this.appVersion);
                Console.WriteLine("Author: " + this.appAuthor + "\nCopyright - " + this.appCopyright + "\n");

                _SIPInited = true;

                loadDevices();

                // Example: set the codec parameter for AMR-WB
                /*

                 _sdkLib.setAudioCodecParameter(AUDIOCODEC_TYPE.AUDIOCODEC_AMRWB, "mode-set=0; octet-align=0; robust-sorting=0");

                */

                Random rd = new Random();
                int LocalSIPPort = rd.Next(10000, 30000); // Generate the random port for SIP

                string ip = getLocalIP();

                // Set the SIP user information
                rt = _sdkLib.setUser(userName,
                                           displayName,
                                           authName,
                                           password,
                                           // Use 0.0.0.0 for local IP then the SDK will choose an available local IP automatically.
                                           // You also can specify a certain local IP to instead of "0.0.0.0", more details please read the SDK User Manual
                                           "0.0.0.0",
                                           LocalSIPPort,
                                           userDomain,
                                           SIPServer,
                                           SIPServerPort,
                                           stunServer,
                                           StunServerPort,
                                           outboundServer,
                                           outboundServerPort);
                if (rt != 0)
                {
                    _sdkLib.unInitialize();
                    _sdkLib.releaseCallbackHandlers();
                    _SIPInited = false;



                    Console.WriteLine("Connection to SIP provider has ended in failure. :(", Console.ForegroundColor = ConsoleColor.Red);
                    return;
                }
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Local Ip Address: " + ip);
                Console.WriteLine("Local Port: " + LocalSIPPort);
                //Output DEVICES
                StringBuilder playDeviceName = new StringBuilder();
                playDeviceName.Length = 256;

                if (_sdkLib.getPlayoutDeviceName(0, playDeviceName, 256) == 0)
                {
                    if (playDeviceName.ToString().Length > 0)
                    {
                        Console.WriteLine("Output Device: " + playDeviceName.ToString());
                    }
                    else {
                        Console.Write("Output Device: ");
                        Console.Write("Not found\n", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                }

                //input devices
                StringBuilder recordDeviceName = new StringBuilder();
                recordDeviceName.Length = 256;

                if (_sdkLib.getRecordingDeviceName(0, recordDeviceName, 256) == 0)
                {
                    if (recordDeviceName.ToString().Length > 0)
                    {
                        Console.WriteLine("Input Device: " + recordDeviceName.ToString());
                    }
                    else {
                        Console.Write("Input Device: ");
                        Console.Write("Not found\n", Console.ForegroundColor = ConsoleColor.Red);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                Console.WriteLine("SIP Trunk #: " + userName);
                Console.WriteLine("SIP Server:  " + SIPServer);
                Console.WriteLine("SIP Port: " + SIPServerPort);

                Console.WriteLine("---------------------------------------------------------");

                SetSRTPType();

                string licenseKey = "PORTSIP_TEST_LICENSE";
                rt = _sdkLib.setLicenseKey(licenseKey);
                if (rt == PortSIP_Errors.ECoreTrialVersionLicenseKey)
                {

                    Console.WriteLine("\n**This sample was built base on evaluation PortSIP VoIP SDK, which allows only three minutes conversation. The conversation will be cut off automatically after three minutes, then you can't hearing anything. Feel free contact us at: sales@portsip.com to purchase the official version.\n", Console.ForegroundColor = ConsoleColor.Yellow);
                }
                else if (rt == PortSIP_Errors.ECoreWrongLicenseKey)
                {
                    Console.WriteLine("\n**The wrong license key was detected, please check with sales@portsip.com or support@portsip.com", Console.ForegroundColor = ConsoleColor.Red);
                }

                InitSettings();
                updatePrackSetting();

                rt = _sdkLib.registerServer(120, 0);
                if (rt != 0)
                {
                    _SIPInited = false;
                    _sdkLib.unInitialize();
                    _sdkLib.releaseCallbackHandlers();

                    Console.WriteLine("register to server failed.", Console.ForegroundColor = ConsoleColor.Red);
                }
                else {
                    Console.WriteLine("Registration Succeeded!");
                }

                try
                {
                    if (args.Length > 0)
                    {
                        if (args[0].StartsWith("call"))
                        {
                            makeCall(args[1]);
                        }
                    }
                }
                catch (Exception) { }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("SIP CONFIGURATION FILE NOT FOUND", Console.ForegroundColor = ConsoleColor.Red);
                Console.WriteLine("KPhone cannot initialize. Please confirm the config.xml file is located in C:\\KPhone.", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }


        }


    }
}
