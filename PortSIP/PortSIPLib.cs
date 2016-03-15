using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using PortSIP;


//////////////////////////////////////////////////////////////////////////
//
//  !!!IMPORTANT!!! DON'T EDIT BELOW SOURCE CODE  
//
//////////////////////////////////////////////////////////////////////////

/*!
*  PortSIP The PortSIP VoIP SDK namespace
*/
namespace PortSIP
{
    /*!
     * @author Copyright (c) 2006-2014 PortSIP Solutions,Inc. All rights reserved.
     * @version 11.2
     * @see http://www.PortSIP.com
     * @class PortSIPLib
     * @brief The PortSIP VoIP SDK class.
 
     PortSIP VoIP SD functions class description.
     */
    unsafe class PortSIPLib
    {

        private  SIPCallbackEvents _SIPCallbackEvents;


        public PortSIPLib(Int32 callbackIndex, Int32 callbackObject, SIPCallbackEvents calbackevents)
        {
            initializeCallbackFunctions();

            _callbackDispatcher = IntPtr.Zero;
            _LibSDK = IntPtr.Zero;
            _callbackObject = callbackObject;
            _callbackIndex = callbackIndex;

            _SIPCallbackEvents = calbackevents;
        }


        public Boolean createCallbackHandlers() // This must called before initialize
        {
            if (_callbackDispatcher != IntPtr.Zero)
            {
                return false;
            }

            if (createSIPCallbackHandle() == false)
            {
                return false;
            }

            setCallbackHandlers();

            return true;

        }


        private Boolean createSIPCallbackHandle()
        {
            _callbackDispatcher = PortSIP_NativeMethods.PSCallback_createCallbackDispatcher();
            if (_callbackDispatcher == IntPtr.Zero)
            {
                return false;
            }

            return true;
        }


        public void releaseCallbackHandlers() // This must called after unInitialize
        {
            if (_callbackDispatcher == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PSCallback_releaseCallbackDispatcher(_callbackDispatcher);
            _callbackDispatcher = IntPtr.Zero;
        }



        private void setCallbackHandlers()
        {
            PortSIP_NativeMethods.PSCallback_setRegisterSuccessHandler(_callbackDispatcher, _rgs, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRegisterFailureHandler(_callbackDispatcher, _rgf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteIncomingHandler(_callbackDispatcher, _ivi, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteTryingHandler(_callbackDispatcher, _ivt, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteSessionProgressHandler(_callbackDispatcher, _ivsp, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteRingingHandler(_callbackDispatcher, _ivr, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteAnsweredHandler(_callbackDispatcher, _iva, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteFailureHandler(_callbackDispatcher, _ivf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteUpdatedHandler(_callbackDispatcher, _ivu, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteConnectedHandler(_callbackDispatcher, _ivc, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteBeginingForwardHandler(_callbackDispatcher, _ivbf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setInviteClosedHandler(_callbackDispatcher, _ivcl, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRemoteHoldHandler(_callbackDispatcher, _rmh, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRemoteUnHoldHandler(_callbackDispatcher, _rmuh, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setReceivedReferHandler(_callbackDispatcher, _rvr, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setReferAcceptedHandler(_callbackDispatcher, _rfa, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setReferRejectedHandler(_callbackDispatcher, _rfr, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setTransferTryingHandler(_callbackDispatcher, _tft, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setTransferRingingHandler(_callbackDispatcher, _tfr, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setACTVTransferSuccessHandler(_callbackDispatcher, _ats, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setACTVTransferFailureHandler(_callbackDispatcher, _atf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setReceivedSignalingHandler(_callbackDispatcher, _rvs, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setSendingSignalingHandler(_callbackDispatcher, _sds, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setWaitingVoiceMessageHandler(_callbackDispatcher, _wvm, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setWaitingFaxMessageHandler(_callbackDispatcher, _wfm, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRecvDtmfToneHandler(_callbackDispatcher, _rdt, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setPresenceRecvSubscribeHandler(_callbackDispatcher, _prs, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setPresenceOnlineHandler(_callbackDispatcher, _pon, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setPresenceOfflineHandler(_callbackDispatcher, _pof, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRecvOptionsHandler(_callbackDispatcher, _rvo, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRecvInfoHandler(_callbackDispatcher, _rvi, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setPlayAudioFileFinishedHandler(_callbackDispatcher, _paf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setPlayVideoFileFinishedHandler(_callbackDispatcher, _pvf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRecvMessageHandler(_callbackDispatcher, _rvm, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setRecvOutOfDialogMessageHandler(_callbackDispatcher, _rodm, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setSendMessageSuccessHandler(_callbackDispatcher, _sms, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setSendMessageFailureHandler(_callbackDispatcher, _smf, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setSendOutOfDialogMessageSuccessHandler(_callbackDispatcher, _sdms, _callbackIndex, _callbackObject);
            PortSIP_NativeMethods.PSCallback_setSendOutOfDialogMessageFailureHandler(_callbackDispatcher, _sdmf, _callbackIndex, _callbackObject);
        }

/** @defgroup groupSDK SDK functions
* SDK functions
* @{
*/
    /** @defgroup group1 Initialize and register functions
    * Initialize and register functions
    * @{
    */

        /*!
         * @brief Initialize the SDK.
         *
         * @param transportType Transport for SIP signaling.TRANSPORT_PERS is the PortSIP private transport for anti the SIP blocking, it must using with the PERS.
         * @param logLevel Set the application log level, the SDK generate the "PortSIP_Log_datatime.log" file if the log enabled.
         * @param logFilePath   The log file path, the path(folder) MUST is exists.
         * @param maxCallLines  In theory support unlimited lines just depends on the device capability, for SIP client recommend less than 1 - 100;
         * @param sipAgent     The User-Agent header to insert in SIP messages.
         * @param audioDeviceLayer            Specifies which audio device layer should be using:<br>
         *            0 = Use the OS default device.<br>
         *            1 = Virtual device    - Virtual device, usually use this for the device which no sound device installed.<br>
         * @param videoDeviceLayer Specifies which video device layer should be using:<br>
         *            0 = Use the OS default device.<br>
         *            1 = Use Virtual device, usually use this for the device which no camera installed.
         * @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code
         */
        public Int32 initialize(TRANSPORT_TYPE transportType,
                                          PORTSIP_LOG_LEVEL logLevel,
                                          String logFilePath,
                                          Int32 maxCallLines,
                                          String sipAgent,
                                          Int32 audioDeviceLayer,
                                          Int32 videoDeviceLayer)
        {
            if (_callbackDispatcher == IntPtr.Zero || _LibSDK != IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            Int32 errorCode = 0;
            _LibSDK = PortSIP_NativeMethods.PortSIP_initialize(_callbackDispatcher,
                                                           (Int32)transportType,
                                                           (Int32)logLevel,
                                                           logFilePath,
                                                           maxCallLines,
                                                           sipAgent,
                                                           audioDeviceLayer,
                                                           videoDeviceLayer,
                                                           out errorCode);

            return errorCode;
        }


        /*!
         *  @brief Un-initialize the SDK and release resources.
         */
        public void unInitialize()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_unInitialize(_LibSDK);
            _LibSDK = IntPtr.Zero;
        }

        /*!
         *  @brief Set user account info.
         *
         *  @param userName           Account(User name) of the SIP, usually provided by an IP-Telephony service provider.
         *  @param displayName        The display name of user, you can set it as your like, such as "James Kend". It's optional.
         *  @param authName           Authorization user name (usually equals the username).
         *  @param password           The password of user, it's optional.
         *  @param localIp            The local computer IP address to bind (for example: 192.168.1.108), it will be using for send and receive SIP message and RTP packet. If pass this IP as the IPv6 format then the SDK using IPv6.
         *  @param localSipPort       The SIP message transport listener port(for example: 5060).
         *  @param userDomain         User domain; this parameter is optional that allow pass a empty string if you are not use domain.
         *  @param sipServer          SIP proxy server IP or domain(for example: xx.xxx.xx.x or sip.xxx.com).
         *  @param sipServerPort      Port of the SIP proxy server, (for example: 5060).
         *  @param stunServer         Stun server, use for NAT traversal, it's optional and can be pass empty string to disable STUN.
         *  @param stunServerPort     STUN server port,it will be ignored if the outboundServer is empty.
         *  @param outboundServer     Outbound proxy server(for example: sip.domain.com), it's optional that allow pass a empty string if not use outbound server.
         *  @param outboundServerPort Outbound proxy server port, it will be ignored if the outboundServer is empty.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setUser(String userName,
                         String displayName,
                         String authName,
                         String password,
                         String localIp,
                         Int32 localSipPort,
                         String userDomain,
                         String sipServer,
                         Int32 sipServerPort,
                         String stunServer,
                         Int32 stunServerPort,
                         String outboundServer,
                         Int32 outboundServerPort)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setUser(_LibSDK,
                                                             userName,
                                                             displayName,
                                                             authName,
                                                             password,
                                                             localIp,
                                                             localSipPort,
                                                             userDomain,
                                                             sipServer,
                                                             sipServerPort,
                                                             stunServer,
                                                             stunServerPort,
                                                             outboundServer,
                                                             outboundServerPort);
        }

        /*!
         *  @brief Register to SIP proxy server(login to server)
         *
         *  @param expires Registration refresh Interval in seconds, maximum is 3600, it will be inserted into SIP REGISTER message headers.
          *  @param retryTimes The retry times if failed to refresh the registration, set to <= 0 the retry will be disabled and onRegisterFailure callback triggered when retry failure.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  if register to server succeeded then onRegisterSuccess will be triggered, otherwise onRegisterFailure triggered.
         */
        public Int32 registerServer(Int32 expires, Int32 retryTimes)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_registerServer(_LibSDK, expires, retryTimes);
        }

        /*!
         *  @brief Un-register from the SIP proxy server.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 unRegisterServer()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_unRegisterServer(_LibSDK);
        }

        /*!
         *  @brief Set the license key, must called before setUser function.
         *
         *  @param key The SDK license key, please purchase from PortSIP
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setLicenseKey(String key)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setLicenseKey(_LibSDK, key);
            
            //return 0;
        }

        /** @} */
        // end of group1

        /** @defgroup group2 NIC and local IP functions
         * @{
         */

        /*!
         *  @brief Get the Network Interface Card numbers.
         *
         *  @return If the function succeeds, the return value is NIC numbers >= 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getNICNums()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNICNums(_LibSDK);
        }

        /*!
         *  @brief Get the local IP address by Network Interface Card index.
         *
         *  @param index The IP address index, for example, the PC has two NICs, we want to obtain the second NIC IP, then set this parameter 1. The first NIC IP index is 0.
         *  @param ip The buffer that to receives the IP. 
         *  @param ipSize The IP buffer size, don't let it less than 32 bytes. 
         *
         *  @return If the function succeeds, the return value is0. If the function fails, the return value is a specific error code. 
         */
        public Int32 getLocalIpAddress(Int32 index, StringBuilder ip, Int32 ipSize)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getLocalIpAddress(_LibSDK, index, ip, ipSize);
        }

        /** @} */
        // end of group2

        /** @defgroup group3 Audio and video codecs functions
         * @{
         */
        /*!
         *  @brief Enable an audio codec, it will be appears in SDP.
         *
         *  @param codecType Audio codec type.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 addAudioCodec(AUDIOCODEC_TYPE codecType)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addAudioCodec(_LibSDK, (Int32)codecType);
        }

        /*!
         *  @brief Enable a video codec, it will be appears in SDP.
         *
         *  @param codecType Video codec type.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 addVideoCodec(VIDEOCODEC_TYPE codecType)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addVideoCodec(_LibSDK, (Int32)codecType);
        }


        /*!
         *  @brief Detect enabled audio codecs is empty or not.
         *
         *  @return If no audio codec was enabled the return value is true, otherwise is false.
         */
        public Boolean isAudioCodecEmpty()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return true;
            }

            return PortSIP_NativeMethods.PortSIP_isAudioCodecEmpty(_LibSDK);
        }


        /*!
         *  @brief Detect enabled video codecs is empty or not.
         *
         *  @return If no video codec was enabled the return value is true, otherwise is false.
         */
        public Boolean isVideoCodecEmpty()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return true;
            }

            return PortSIP_NativeMethods.PortSIP_isVideoCodecEmpty(_LibSDK);
        }

        /*!
         *  @brief Set the RTP payload type for dynamic audio codec.
         *
         *  @param codecType   Audio codec type, defined in the PortSIPTypes file.
         *  @param payloadType The new RTP payload type that you want to set.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setAudioCodecPayloadType(AUDIOCODEC_TYPE codecType, Int32 payloadType)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioCodecPayloadType(_LibSDK, (Int32)codecType, payloadType);
        }

        /*!
         *  @brief Set the RTP payload type for dynamic Video codec.
         *
         *  @param codecType   Video codec type, defined in the PortSIPTypes file.
         *  @param payloadType The new RTP payload type that you want to set.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoCodecPayloadType(VIDEOCODEC_TYPE codecType, Int32 payloadType)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoCodecPayloadType(_LibSDK, (Int32)codecType, payloadType);
        }


        /*!
         *  @brief Remove all enabled audio codecs.
         */
        public void clearAudioCodec()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearAudioCodec(_LibSDK);
        }

        /*!
         *  @brief Remove all enabled video codecs.
         */
        public void clearVideoCodec()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_clearVideoCodec(_LibSDK);
        }

        /*!
         *  @brief Set the codec parameter for audio codec.
         *
         *  @param codecType Audio codec type, defined in the PortSIPTypes file.
         *  @param parameter The parameter in string format.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         * @remark Example:
         *@code setAudioCodecParameter(AUDIOCODEC_AMR, "mode-set=0; octet-align=1; robust-sorting=0"); @endcode
         */
        public Int32 setAudioCodecParameter(AUDIOCODEC_TYPE codecType, String parameter)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioCodecParameter(_LibSDK, (Int32)codecType, parameter);
        }

        /*!
         *  @brief Set the codec parameter for video codec.
         *
         *  @param codecType Video codec type, defined in the PortSIPTypes file.
         *  @param parameter The parameter in string format.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         * @remark Example:
         *@code setVideoCodecParameter(VIDEO_CODEC_H264, "profile-level-id=420033; packetization-mode=0"); @endcode
         */
        public Int32 setVideoCodecParameter(VIDEOCODEC_TYPE codecType, String parameter)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoCodecParameter(_LibSDK, (Int32)codecType, parameter);
        }
        /** @} */
        // end of group3

        /** @defgroup group4 Additional setting functions
         * @{
         */

        /*!
         *  @brief Set user display name.
         *
         *  @param name The display name.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setDisplayName(String name)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setDisplayName(_LibSDK, name);
        }


        /*!
         *  @brief Get the current version number of the SDK.
         *
         *  @param majorVersion Return the major version number.
         *  @param minorVersion Return the minor version number.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getVersion(out Int32 majorVersion, out Int32 minorVersion)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                majorVersion = 0;
                minorVersion = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getVersion(_LibSDK, out majorVersion, out minorVersion);
        }

        /*!
         *  @brief Enable/disable PRACK.
         *
         *  @param enable enable Set to true to enable the SDK support PRACK, default the PRACK is disabled.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 enableReliableProvisional(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableReliableProvisional(_LibSDK, enable);
        }

        /*!
         *  @brief Enable/disable the 3Gpp tags, include "ims.icsi.mmtel" and "g.3gpp.smsip".
         *
         *  @param enable enable Set to true to enable the SDK support 3Gpp tags.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 enable3GppTags(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enable3GppTags(_LibSDK, enable);
        }

        /*!
         *  @brief Enable/disable callback the sending SIP messages.
         *
         *  @param enable enable Set as true to enable callback the sent SIP messages, false to disable. Once enabled,the "onSendingSignaling" event will be fired once the SDK sending a SIP message.
         */
        public void enableCallbackSendingSignaling(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableCallbackSendingSignaling(_LibSDK, enable);
        }

        /*!
         *  @brief Set the SRTP policy.
         *
         *  @param srtpPolicy The SRTP policy.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setSrtpPolicy(SRTP_POLICY srtpPolicy)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSrtpPolicy(_LibSDK, (Int32)srtpPolicy);
        }

        /*!
         *  @brief Set the RTP ports range for audio and video streaming.
         *
         *  @param minimumRtpAudioPort The minimum RTP port for audio stream.
         *  @param maximumRtpAudioPort The maximum RTP port for audio stream.
         *  @param minimumRtpVideoPort The minimum RTP port for video stream.
         *  @param maximumRtpVideoPort The maximum RTP port for video stream.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark
         *  The port range((max - min) % maxCallLines) should more than 4.
         */
        public Int32 setRtpPortRange(Int32 minimumRtpAudioPort, Int32 maximumRtpAudioPort, Int32 minimumRtpVideoPort, Int32 maximumRtpVideoPort)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRtpPortRange(_LibSDK,
                                                         minimumRtpAudioPort,
                                                         maximumRtpAudioPort,
                                                         minimumRtpVideoPort,
                                                         maximumRtpVideoPort);
        }

        /*!
         *  @brief Set the RTCP ports range for audio and video streaming.
         *
         *  @param minimumRtcpAudioPort The minimum RTCP port for audio stream.
         *  @param maximumRtcpAudioPort The maximum RTCP port for audio stream.
         *  @param minimumRtcpVideoPort The minimum RTCP port for video stream.
         *  @param maximumRtcpVideoPort The maximum RTCP port for video stream.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark
         *  The port range((max - min) % maxCallLines) should more than 4.
         */
        public Int32 setRtcpPortRange(Int32 minimumRtcpAudioPort, Int32 maximumRtcpAudioPort, Int32 minimumRtcpVideoPort, Int32 maximumRtcpVideoPort)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRtcpPortRange(_LibSDK,
                                                         minimumRtcpAudioPort,
                                                         maximumRtcpAudioPort,
                                                         minimumRtcpVideoPort,
                                                         maximumRtcpVideoPort);
        }

        /*!
         *  @brief Enable call forward.
         *
         *  @param forBusyOnly If set this parameter as true, the SDK will forward all incoming calls when currently it's busy. If set this as false, the SDK forward all inconing calls anyway.
         *  @param forwardTo   The call forward target, it's must likes sip:xxxx@sip.portsip.com.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 enableCallForward(Boolean forBusyOnly, String forwardTo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableCallForward(_LibSDK, forBusyOnly, forwardTo);
        }

        /*!
         *  @brief Disable the call forward, the SDK is not forward any incoming call after this function is called.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 disableCallForward()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_disableCallForward(_LibSDK);
        }

        /*!
         *  @brief Allows to periodically refresh Session Initiation Protocol (SIP) sessions by sending repeated INVITE requests.
         *
         *  @param timerSeconds The value of the refresh interval in seconds. Minimum requires 90 seconds.
         *  @param refreshMode  Allow set the session refresh by UAC or UAS: SESSION_REFERESH_UAC or SESSION_REFERESH_UAS;
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark The repeated INVITE requests, or re-INVITEs, are sent during an active call leg to allow user agents (UA) or proxies to determine the status of a SIP session. 
         *  Without this keepalive mechanism, proxies that remember incoming and outgoing requests (stateful proxies) may continue to retain call state needlessly. 
         *  If a UA fails to send a BYE message at the end of a session or if the BYE message is lost because of network problems, a stateful proxy does not know that the session has ended. 
         *  The re-INVITES ensure that active sessions stay active and completed sessions are terminated.
         */
        public Int32 enableSessionTimer(Int32 timerSeconds, SESSION_REFRESH_MODE refreshMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableSessionTimer(_LibSDK, timerSeconds, (Int32)refreshMode);
        }

        /*!
         *  @brief Disable the session timer.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 disableSessionTimer()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_disableSessionTimer(_LibSDK);
        }

        /*!
         *  @brief Enable the "Do not disturb" to enable/disable.
         *
         *  @param state If set to true, the SDK reject all incoming calls anyway.
         */
        public void setDoNotDisturb(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setDoNotDisturb(_LibSDK, state);
        }

        /*!
         *  @brief Use to obtain the MWI status.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 detectMwi()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_detectMwi(_LibSDK);
        }

        /*!
         *  @brief Allows enable/disable the check MWI(Message Waiting Indication).
         *
         *  @param state If set as true will check MWI automatically once successfully registered to a SIP proxy server.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 enableCheckMwi(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableCheckMwi(_LibSDK, state);
        }

        /*!
         *  @brief Enable or disable send RTP keep-alive packet during the call is established.
         *
         *  @param state                Set to true allow send the keep-alive packet during the conversation.
         *  @param keepAlivePayloadType The payload type of the keep-alive RTP packet, usually set to 126.
         *  @param deltaTransmitTimeMS  The keep-alive RTP packet send interval, in millisecond, usually recommend 15000 - 300000.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setRtpKeepAlive(Boolean state, Int32 keepAlivePayloadType, Int32 deltaTransmitTimeMS)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRtpKeepAlive(_LibSDK, state, keepAlivePayloadType, deltaTransmitTimeMS);
        }

        /*!
         *  @brief Enable or disable send SIP keep-alive packet.
         *
         *  @param keepAliveTime This is the SIP keep alive time interval in seconds, set to 0 to disable the SIP keep alive, it's in seconds, recommend 30 or 50.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setKeepAliveTime(Int32 keepAliveTime)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setKeepAliveTime(_LibSDK, keepAliveTime);
        }

        /*!
         *  @brief Set the audio capture sample.
         *
         *  @param ptime    It's should be a multiple of 10, and between 10 - 60(included 10 and 60).
         *  @param maxPtime For the "maxptime" attribute, should be a multiple of 10, and between 10 - 60(included 10 and 60). Can't less than "ptime".
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark which will be appears in the SDP of INVITE and 200 OK message as "ptime and "maxptime" attribute.
         */
        public Int32 setAudioSamples(Int32 ptime, Int32 maxPtime)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioSamples(_LibSDK, ptime, maxPtime);
        }

        /*!
         *  @brief Set the SDK receive the SIP message that include special mime type.
         *
         *  @param methodName  Method name of the SIP message, likes INVITE, OPTION, INFO, MESSAGE, UPDATE, ACK etc. More details please read the RFC3261.
         *  @param mimeType    The mime type of SIP message.
         *  @param subMimeType The sub mime type of SIP message.
         *  
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *@remark         
         * Default, the PortSIP VoIP SDK support these media types(mime types) that in the below incoming SIP messages:
         * @code
                        "message/sipfrag" in NOTIFY message.
                        "application/simple-message-summary" in NOTIFY message.
                        "text/plain" in MESSAGE message.
                        "application/dtmf-relay" in INFO message.
                        "application/media_control+xml" in INFO message.
         * @endcode
         * The SDK allows received SIP message that included above mime types. Now if remote side send a INFO
         * SIP message, this message "Content-Type" header value is "text/plain", the SDK will reject this INFO message,
         * because "text/plain" of INFO message does not included in the default support list.
         * Then how to let the SDK receive the SIP INFO message that included "text/plain" mime type? We should use
         * addSupportedMimyType to do it:
         * @code
                        addSupportedMimeType("INFO", "text", "plain");
         * @endcode
         * If want to receive the NOTIFY message with "application/media_control+xml", then:
         *@code
                        addSupportedMimeType("NOTIFY", "application", "media_control+xml");
         * @endcode
         * About the mime type details, please visit this website: http://www.iana.org/assignments/media-types/ 
         */
        public Int32 addSupportedMimeType(String methodName, String mimeType, String subMimeType)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addSupportedMimeType(_LibSDK, methodName, mimeType, subMimeType);
        }

        /** @} */
        // end of group4

        /** @defgroup group5 Access SIP message header functions
         * @{
         */

        /*!
         *  @brief Access the SIP header of SIP message.
         *
         *  @param sipMessage        The SIP message.
         *  @param headerName        Which header want to access of the SIP message.
         *  @param headerValue       The buffer to receive header value.
         *  @param headerValueLength The headerValue buffer size. Usually we recommended set it more than 512 bytes.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         * @remark
         * When got a SIP message in the onReceivedSignaling callback event, and want to get SIP message header value, use getExtensionHeaderValue to do it:
         * @code
            StringBuilder value = new StringBuilder();
            value.Length = 512;
            getExtensionHeaderValue(message, name, value);
         * @endcode
         */
        public Int32 getExtensionHeaderValue(String sipMessage, String headerName, StringBuilder headerValue, Int32 headerValueLength)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getExtensionHeaderValue(_LibSDK, sipMessage, headerName, headerValue, headerValueLength);
        }

        /*!
         *  @brief Add the extension header(custom header) into every outgoing SIP message.
         *
         *  @param headerName  The custom header name which will be appears in every outgoing SIP message.
         *  @param headerValue The custom header value.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 addExtensionHeader(String headerName, String headerValue)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_addExtensionHeader(_LibSDK, headerName, headerValue);
        }

        /*!
         *  @brief Clear the added extension headers(custom headers)
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark Example, we have added two custom headers into every outgoing SIP message and want remove them.
         * @code
            addExtensionHeader("Blling", "usd100.00");	
            addExtensionHeader("ServiceId", "8873456");
            clearAddextensionHeaders();
         * @endcode
         */
        public Int32 clearAddExtensionHeaders()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_clearAddExtensionHeaders(_LibSDK);
        }

        /*!
         *  @brief Modify the special SIP header value for every outgoing SIP message.
         *
         *  @param headerName  The SIP header name which will be modify it's value.
         *  @param headerValue The heaver value want to modify.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 modifyHeaderValue(String headerName, String headerValue)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_modifyHeaderValue(_LibSDK, headerName, headerValue);
        }

        /*!
         *  @brief Clear the modify headers value, no longer modify every outgoing SIP message header values.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark  Example, modified two headers value for every outging SIP message and then clear it:
         @code
            modifyHeaderValue("Expires", "1000");	
            modifyHeaderValue("User-Agent", "MyTest Softphone 1.0");
            cleaModifyHeaders();
         @endcode
         */
        public Int32 clearModifyHeaders()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_clearModifyHeaders(_LibSDK);
        }
        /** @} */
        // end of group5

        /** @defgroup group6 Audio and video functions
         * @{
         */

        /*!
         *  @brief Set the audio device that will use for audio call. 
         *
         *  @param recordingDeviceId    Device ID(index) for audio record.(Microphone). 
         *  @param playoutDeviceId      Device ID(index) for audio playback(Speaker). 
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setAudioDeviceId(Int32 recordingDeviceId, Int32 playoutDeviceId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioDeviceId(_LibSDK, recordingDeviceId, playoutDeviceId);
        }

        /*!
         *  @brief Set the video device that will use for video call.
         *
         *  @param deviceId Device ID(index) for video device(camera).
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoDeviceId(Int32 deviceId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoDeviceId(_LibSDK, deviceId);
        }

        /*!
         *  @brief Set the video capture resolution.
         *
         *  @param resolution Video resolution, defined in PortSIPType file. Note: Some cameras don't support SVGA and XVGA, 720P, please read your camera manual.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoResolution(VIDEO_RESOLUTION resolution)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoResolution(_LibSDK, (Int32)resolution);
        }

        /*!
         *  @brief Set the video bit rate.
         *
         *  @param bitrateKbps The video bit rate in KBPS.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoBitrate(Int32 bitrateKbps)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoBitrate(_LibSDK, bitrateKbps);
        }

        /*!
         *  @brief Set the video frame rate. 
         *
         *  @param frameRate The frame rate value, minimum is 5, maximum is 30. The bigger value will give you better video quality but require more bandwidth.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark Usually you do not need to call this function set the frame rate, the SDK using default frame rate.
         */
        public Int32 setVideoFrameRate(Int32 frameRate)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoFrameRate(_LibSDK, frameRate);
        }

        /*!
         *  @brief Send the video to remote side.
         *
         *  @param sessionId The session ID of the call.
         *  @param sendState Set to true to send the video, false to stop send.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 sendVideo(Int32 sessionId, Boolean sendState)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendVideo(_LibSDK, sessionId, sendState);
        }

        /*!
         *  @brief Changing the orientation of the video.
         *
         *  @param rotation  The video rotation that you want to set(0,90,180,270).
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoOrientation(Int32 rotation)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoOrientation(_LibSDK, rotation);
        }

        /*!
         *  @brief Set the the window that using to display the local video image.
         *
         *  @param localVideoWindow The window to display local video image from camera. 
         */
        public void setLocalVideoWindow(IntPtr localVideoWindow)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_setLocalVideoWindow(_LibSDK, localVideoWindow);
        }

        /*!
         *  @brief Set the window for a session that using to display the received remote video image.
         *
         *  @param sessionId         The session ID of the call.
         *  @param remoteVideoWindow The window to display received remote video image. 
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setRemoteVideoWindow(Int32 sessionId, IntPtr remoteVideoWindow)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setRemoteVideoWindow(_LibSDK, sessionId, remoteVideoWindow);
        }

        /*!
         *  @brief Start/stop to display the local video image.
         *
         *  @param state state Set to true to display local video iamge.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 displayLocalVideo(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_displayLocalVideo(_LibSDK, state);
        }

        /*!
         *  @brief Enable/disable the NACK feature(rfc6642) which help to improve the video quatliy.
         *
         *  @param state state Set to true to enable.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoNackStatus(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoNackStatus(_LibSDK, state);
        }

        /*!
         *  @brief Mute the device microphone.it's unavailable for Android and iOS.
         *
         *  @param mute If the value is set to true, the microphone is muted, set to false to un-mute it.
         */
        public void muteMicrophone(Boolean mute)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_muteMicrophone(_LibSDK, mute);
        }

        /*!
         *  @brief Mute the device speaker, it's unavailable for Android and iOS.
         *
         *  @param mute If the value is set to true, the speaker is muted, set to false to un-mute it.
         */
        public void muteSpeaker(Boolean mute)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_muteSpeaker(_LibSDK, mute);
        }

        /*!
         *  @brief Obtain the dynamic microphone volume level from current call. 
         *
         *  @param speakerVolume    Return the dynamic speaker volume by this parameter, the range is 0 - 9.
         *  @param microphoneVolume Return the dynamic microphone volume by this parameter, the range is 0 - 9.
         *  @remark Usually set a timer to call this function to refresh the volume level indicator.
         */
        public void getDynamicVolumeLevel(out Int32 speakerVolume, out Int32 microphoneVolume)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                speakerVolume = 0;
                microphoneVolume = 0;

                return;
            }

            PortSIP_NativeMethods.PortSIP_getDynamicVolumeLevel(_LibSDK, out speakerVolume, out microphoneVolume);
        }

        /** @} */
        // end of group6

        /** @defgroup group7 Call functions
         * @{
         */

        /*!
         *  @brief Make a call
         *
         *  @param callee    The callee, it can be name only or full SIP URI, for example: user001 or sip:user001@sip.iptel.org or sip:user002@sip.yourdomain.com:5068
         *  @param sendSdp   If set to false then the outgoing call doesn't include the SDP in INVITE message.
         *  @param videoCall If set the true and at least one video codec was added, then the outgoing call include the video codec into SDP.
         *
         *  @return If the function succeeds, the return value is the session ID of the call greater than 0. If the function fails, the return value is a specific error code. Note: the function success just means the outgoing call is processing, you need to detect the call final state in onInviteTrying, onInviteRinging, onInviteFailure callback events.
         */
        public Int32 call(String callee, Boolean sendSdp, Boolean videoCall)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.INVALID_SESSION_ID;
            }

            return PortSIP_NativeMethods.PortSIP_call(_LibSDK, callee, sendSdp, videoCall);

        }

        /*!
         *  @brief rejectCall Reject the incoming call.
         *
         *  @param sessionId The sessionId of the call.
         *  @param code      Reject code, for example, 486, 480 etc.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 rejectCall(Int32 sessionId, int code)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_rejectCall(_LibSDK, sessionId, code);
        }

        /*!
         *  @brief hangUp Hang up the call.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 hangUp(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_hangUp(_LibSDK, sessionId);
        }

        /*!
         *  @brief answerCall Answer the incoming call.
         *
         *  @param sessionId The session ID of call.
         *  @param videoCall If the incoming call is a video call and the video codec is matched, set to true to answer the video call.<br>If set to false, the answer call doesn't include video codec answer anyway.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 answerCall(Int32 sessionId, Boolean videoCall)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_answerCall(_LibSDK, sessionId, videoCall);
        }

        /*!
         *  @brief Use the re-INVITE to update the established call.
         *  @param sessionId   The session ID of call.
         *  @param enableAudio Set to true to allow the audio in update call, false for disable audio in update call.
         *  @param enableVideo Set to true to allow the video in update call, false for disable video in update call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark
            Example usage:<br>
         *  Example 1: A called B with the audio only, B answered A, there has an audio conversation between A, B. Now A want to see B video, 
            A use these functions to do it.
            @code
                        clearVideoCodec();	
                        addVideoCodec(VIDEOCODEC_H264);
                        updateCall(sessionId, true, true);
            @endcode
            Example 2: Remove video stream from currently conversation. 
            @code
                        updateCall(sessionId, true, false);
            @endcode
         */
        public Int32 updateCall(Int32 sessionId, bool enableAudio, bool enableVideo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_updateCall(_LibSDK, sessionId, enableAudio, enableVideo);
        }

        /*!
         *  @brief To place a call on hold.
         *
         *  @param sessionId The session ID of call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 hold(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_hold(_LibSDK, sessionId);
        }

        /*!
         *  @brief Take off hold.
         *
         *  @param sessionId The session ID of call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 unHold(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_unHold(_LibSDK, sessionId);
        }

        /*!
         *  @brief Mute the specified session audio or video.
         *
         *  @param sessionId         The session ID of the call.
         *  @param muteIncomingAudio Set it to true to mute incoming audio stredam, can't hearing remote side audio.
         *  @param muteOutgoingAudio Set it to true to mute outgoing audio stredam, the remote side can't hearing audio.
         *  @param muteIncomingVideo Set it to true to mute incoming video stredam, can't see remote side video.
         *  @param muteOutgoingVideo Set it to true to mute outgoing video stredam, the remote side can't see video.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 muteSession(Int32 sessionId,
                                Boolean muteIncomingAudio,
                                Boolean muteOutgoingAudio,
                                Boolean muteIncomingVideo,
                                Boolean muteOutgoingVideo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_muteSession(_LibSDK, sessionId, muteIncomingAudio, muteOutgoingAudio, muteIncomingVideo, muteOutgoingVideo);
        }

        /*!
         *  @brief Forward call to another one when received the incoming call.
         *
         *  @param sessionId The session ID of the call.
         *  @param forwardTo Target of the forward, it can be "sip:number@sipserver.com" or "number" only.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 forwardCall(Int32 sessionId, String forwardTo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_forwardCall(_LibSDK, sessionId, forwardTo);
        }

        /*!
         *  @brief Send DTMF tone.
         *
         *  @param sessionId    The session ID of the call.
         *  @param dtmfMethod   Support send DTMF tone with two methods: DTMF_RFC2833 and DTMF_INFO. The DTMF_RFC2833 is recommend.
         *  @param code         The DTMF tone(0-16).
         * <p><table>
         * <tr><th>code</th><th>Description</th></tr>
         * <tr><td>0</td><td>The DTMF tone 0.</td></tr><tr><td>1</td><td>The DTMF tone 1.</td></tr><tr><td>2</td><td>The DTMF tone 2.</td></tr>
         * <tr><td>3</td><td>The DTMF tone 3.</td></tr><tr><td>4</td><td>The DTMF tone 4.</td></tr><tr><td>5</td><td>The DTMF tone 5.</td></tr>
         * <tr><td>6</td><td>The DTMF tone 6.</td></tr><tr><td>7</td><td>The DTMF tone 7.</td></tr><tr><td>8</td><td>The DTMF tone 8.</td></tr>
         * <tr><td>9</td><td>The DTMF tone 9.</td></tr><tr><td>10</td><td>The DTMF tone *.</td></tr><tr><td>11</td><td>The DTMF tone #.</td></tr>
         * <tr><td>12</td><td>The DTMF tone A.</td></tr><tr><td>13</td><td>The DTMF tone B.</td></tr><tr><td>14</td><td>The DTMF tone C.</td></tr>
         * <tr><td>15</td><td>The DTMF tone D.</td></tr><tr><td>16</td><td>The DTMF tone FLASH.</td></tr>
         * </table></p>
         *  @param dtmfDuration The DTMF tone samples, recommend 160.
         *  @param playDtmfTone Set to true the SDK play local DTMF tone sound during send DTMF.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 sendDtmf(Int32 sessionId, DTMF_METHOD dtmfMethod, int code, int dtmfDuration, bool playDtmfTone)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendDtmf(_LibSDK, sessionId, (Int32)dtmfMethod, code, dtmfDuration, playDtmfTone);
        }
        /** @} */
        // end of group7

        /** @defgroup group8 Refer functions
         * @{
         */

        /*!
         *  @brief Refer the currently call to another one.<br>
         *  @param sessionId The session ID of the call.
         *  @param referTo   Target of the refer, it can be "sip:number@sipserver.com" or "number" only.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark
         @code
            refer(sessionId, "sip:testuser12@sip.portsip.com");
         @endcode
         You can download the demo AVI at:<br>
         "http://www.portsip.com/downloads/video/blindtransfer.rar", use the Windows Media
         Player to play the AVI file after extracted, it will shows how to do the transfer.
         */
        public Int32 refer(Int32 sessionId, String referTo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_refer(_LibSDK, sessionId, referTo);
        }

        /*!
         *  @brief  Make an attended refer.
         *
         *  @param sessionId        The session ID of the call.
         *  @param replaceSessionId Session ID of the replace call.
         *  @param referTo          Target of the refer, it can be "sip:number@sipserver.com" or "number" only.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark
            Please read the sample project source code to got more details. Or download the demo AVI at:"http://www.portsip.com/downloads/video/blindtransfer.rar"<br>
           use the Windows Media Player to play the AVI file after extracted, it will shows how to do the transfer.
         */
        public Int32 attendedRefer(Int32 sessionId, Int32 replaceSessionId, String referTo)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_attendedRefer(_LibSDK, sessionId, replaceSessionId, referTo);
        }

        /*!
         *  @brief Accept the REFER request, a new call will be make if called this function, usuall called after onReceivedRefer callback event.
         *
         *  @param referId        The ID of REFER request that comes from onReceivedRefer callback event.
         *  @param referSignalingMessage The SIP message of REFER request that comes from onReceivedRefer callback event.
         *
         *  @return If the function succeeds, the return value is a session ID greater than 0 to the new call for REFER, otherwise is a specific error code less than 0.
         */
        public Int32 acceptRefer(Int32 referId, String referSignalingMessage)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_acceptRefer(_LibSDK, referId, referSignalingMessage);
        }

        /*!
         *  @brief Reject the REFER request.
         *
         *  @param referId The ID of REFER request that comes from onReceivedRefer callback event.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 rejectRefer(Int32 referId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_rejectRefer(_LibSDK, referId);
        }
        /** @} */
        // end of group8

        /** @defgroup group9 Send audio and video stream functions
         * @{
         */

        /*!
         *  @brief Enable the SDK send PCM stream data to remote side from another source to instread of microphone.
         *
         *  @param sessionId           The session ID of call.
         *  @param state               Set to true to enable the send stream, false to disable.
         *  @param streamSamplesPerSec The PCM stream data sample in seconds, for example: 8000 or 16000.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark MUST called this function first if want to send the PCM stream data to another side.
         */
        public Int32 enableSendPcmStreamToRemote(Int32 sessionId, Boolean state, Int32 streamSamplesPerSec)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableSendPcmStreamToRemote(_LibSDK, sessionId, state, streamSamplesPerSec);
        }

        /*!
         *  @brief Send the audio stream in PCM format from another source to instead of audio device capture(microphone).
         *
         *  @param sessionId Session ID of the call conversation.
         *  @param data        The PCM audio stream data, must is 16bit, mono.
         *  @param dataLength  The size of data. 
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark Usually we should use it like below:
         *  @code
                        enableSendPcmStreamToRemote(sessionId, true, 16000);	
                        sendPcmStreamToRemote(sessionId, data, dataSize);
         *  @endcode
         */
        public Int32 sendPcmStreamToRemote(Int32 sessionId, byte[] data, Int32 dataLength)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendPcmStreamToRemote(_LibSDK, sessionId, data, dataLength);
        }

        /*!
         *  @brief Enable the SDK send video stream data to remote side from another source to instread of camera.
         *
         *  @param sessionId The session ID of call.
         *  @param state     Set to true to enable the send stream, false to disable.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 enableSendVideoStreamToRemote(Int32 sessionId, Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableSendVideoStreamToRemote(_LibSDK, sessionId, state);
        }

        /*!
         *  @brief Send the video stream to remote
         *
         *  @param sessionId Session ID of the call conversation.
         *  @param data      The video video stream data, must is i420 format.
         *  @param dataLength The size of data. 
         *  @param width     The video image width.
         *  @param height    The video image height.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark  Send the video stream in i420 from another source to instead of video device capture(camera).<br>
         Before called this funtion,you MUST call the enableSendVideoStreamToRemote function.<br>
         * Usually we should use it like below:
         @code
                    enableSendVideoStreamToRemote(sessionId, true);	
                    sendVideoStreamToRemote(sessionId, data, dataSize, 352, 288);
         @endcode
         */
        public Int32 sendVideoStreamToRemote(Int32 sessionId, byte[] data, Int32 dataLength, Int32 width, Int32 height)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendVideoStreamToRemote(_LibSDK, sessionId, data, dataLength, width, height);
        }

        /** @} */
        // end of group9

        /** @defgroup group10 RTP packets, Audio stream and video stream callback functions
         * @{
         */

        /*!
         *  @brief Set the RTP callbacks to allow access the sending and received RTP packets.
         *
         *  @param callbackObject The callback object that you passed in and can access it once callback function triggered.
         *  @param enable Set to true to enable the RTP callback for received and sending RTP packets, the onSendingRtpPacket and onReceivedRtpPacket events will be triggered.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setRtpCallback(Int32 callbackObject, Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            if (enable == true)
            {
                return PortSIP_NativeMethods.PortSIP_setRtpCallback(_LibSDK, (IntPtr)callbackObject, _rrc, _src);
            }

            return PortSIP_NativeMethods.PortSIP_setRtpCallback(_LibSDK, (IntPtr)callbackObject, null, null);
        }

        /*!
         *  @brief Enable/disable the audio stream callback
         *
         *  @param callbackObject The callback object that you passed in and can access it once callback function triggered.
         *  @param sessionId    The session ID of call.
         *  @param enable       Set to true to enable audio stream callback, false to stop the callback.
         *  @param callbackMode The audio stream callback mode
         * <p><table>
         * <tr><th>Mode</th><th>Description</th></tr>
         * <tr><td>AUDIOSTREAM_LOCAL_MIX</td><td>Callback the audio stream from microphone for all channels.  </td></tr>
         * <tr><td>AUDIOSTREAM_LOCAL_PER_CHANNEL</td><td>Callback the audio stream from microphone for one channel base on the given sessionId. </td></tr>
         * <tr><td>AUDIOSTREAM_REMOTE_MIX</td><td>Callback the received audio stream that mixed including all channels. </td></tr>
         * <tr><td>AUDIOSTREAM_REMOTE_PER_CHANNEL</td><td>Callback the received audio stream for one channel base on the given sessionId.</td></tr>
         * </table></p>
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark the onAudioRawCallback event will be triggered if the callback is enabled.
         */
        public Int32 enableAudioStreamCallback(Int32 callbackObject, Int32 sessionId, Boolean enable, AUDIOSTREAM_CALLBACK_MODE callbackMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_enableAudioStreamCallback(_LibSDK, sessionId, enable, (Int32)callbackMode, (IntPtr)callbackObject, _arc);
        }

        /*!
         *  @brief Enable/disable the video stream callback.
         *
         *  @param callbackObject The callback object that you passed in and can access it once callback function triggered.
         *  @param sessionId    The session ID of call.
         *  @param callbackMode The video stream callback mode.
         * <p><table>
         * <tr><th>Mode</th><th>Description</th></tr>
         * <tr><td>VIDEOSTREAM_NONE</td><td>Disable video stream callback. </td></tr>
         * <tr><td>VIDEOSTREAM_LOCAL</td><td>Local video stream callback. </td></tr>
         * <tr><td>VIDEOSTREAM_REMOTE</td><td>Remote video stream callback. </td></tr>
         * <tr><td>VIDEOSTREAM_BOTH</td><td>Both of local and remote video stream callback. </td></tr>
         * </table></p>
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         *  @remark the onVideoRawCallback event will be triggered if the callback is enabled.
         */
        public Int32 enableVideoStreamCallback(Int32 callbackObject, Int32 sessionId, VIDEOSTREAM_CALLBACK_MODE callbackMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }


            return PortSIP_NativeMethods.PortSIP_enableVideoStreamCallback(_LibSDK, sessionId, (Int32)callbackMode, (IntPtr)callbackObject, _vrc);
        }

        /** @} */
        // end of group10

        /** @defgroup group11 Record functions
         * @{
         */

        /*!
         *  @brief Start record the call.
         *
         *  @param sessionId        The session ID of call conversation.
         *  @param recordFilePath   The file path to save record file, it's must exists.
         *  @param recordFileName   The file name of record file, for example: audiorecord.wav or videorecord.avi.
         *  @param appendTimestamp  Set to true to append the timestamp to the recording file name.
         *  @param audioFileFormat  The audio record file format.
         *  @param audioRecordMode  The audio record mode.
         *  @param videoFileCodecType The codec which using for compress the video data to save into video record file.
         *  @param videoRecordMode  Allow set video record mode, support record received video/send video/both received and send.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 startRecord(Int32 sessionId,
                                String recordFilePath,
                                String recordFileName,
                                Boolean appendTimestamp,
                                AUDIO_RECORDING_FILEFORMAT audioFileFormat,
                                RECORD_MODE audioRecordMode,
                                VIDEOCODEC_TYPE videoFileCodecType,
                                RECORD_MODE videoRecordMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_startRecord(_LibSDK,
                                                            sessionId,
                                                            recordFilePath, 
                                                            recordFileName, 
                                                            appendTimestamp,
                                                            (Int32)audioFileFormat,
                                                            (Int32)audioRecordMode,
                                                            (Int32)videoFileCodecType,
                                                            (Int32)videoRecordMode);
        }

        /*!
         *  @brief Stop record.
         *
         *  @param sessionId The session ID of call conversation.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 stopRecord(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_stopRecord(_LibSDK, sessionId);
        }
        /** @} */
        // end of group11

        /** @defgroup group12 Play audio and video file to remoe functions
         * @{
         */


        /*!
         *  @brief Play an AVI file to remote party.
         *
         *  @param sessionId Session ID of the call.
         *  @param fileName   The file full path name, such as "c:\\test.avi".
         *  @param loop      Set to false to stop play video file when it is end. Set to true to play it as repeat.
         *  @param playAudio If set to true then play audio and video together, set to false just play video only.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 playVideoFileToRemote(Int32 sessionId, String fileName, Boolean loop, Boolean playAudio)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull; ;
            }

            return PortSIP_NativeMethods.PortSIP_playVideoFileToRemote(_LibSDK, sessionId, fileName, loop, playAudio);
        }

        /*!
         *  @brief Stop play video file to remote side.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 stopPlayVideoFileToRemote(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull; ;
            }

            return PortSIP_NativeMethods.PortSIP_stopPlayVideoFileToRemote(_LibSDK, sessionId);
        }

        /*!
         *  @brief Play an wave file to remote party.
         *
         *  @param sessionId         Session ID of the call.
         *  @param fileName          The file full path name, such as "c:\\test.wav".
         *  @param fileSamplesPerSec The wave file sample in seconds, should be 8000 or 16000 or 32000.
         *  @param loop              Set to false to stop play audio file when it is end. Set to true to play it as repeat.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 playAudioFileToRemote(Int32 sessionId, String fileName, Int32 fileSamplesPerSec, Boolean loop)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;;
            }
            
           return PortSIP_NativeMethods.PortSIP_playAudioFileToRemote(_LibSDK, sessionId, fileName, fileSamplesPerSec, loop);
        }

        /*!
         *  @brief Stop play wave file to remote side.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 stopPlayAudioFileToRemote(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull; ;
            }

            return PortSIP_NativeMethods.PortSIP_stopPlayAudioFileToRemote(_LibSDK, sessionId);
        }

        /*!
         *  @brief Play an wave file to remote party as conversation background sound.
         *
         *  @param sessionId         Session ID of the call.
         *  @param fileName          The file full path name, such as "c:\\test.wav".
         *  @param fileSamplesPerSec The wave file sample in seconds, should be 8000 or 16000 or 32000.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 playAudioFileToRemoteAsBackground(Int32 sessionId, String fileName, Int32 fileSamplesPerSec)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull; ;
            }

            return PortSIP_NativeMethods.PortSIP_playAudioFileToRemoteAsBackground(_LibSDK, sessionId, fileName, fileSamplesPerSec);
        }

        /*!
         *  @brief Stop play an wave file to remote party as conversation background sound.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 stopPlayAudioFileToRemoteAsBackground(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull; ;
            }

            return PortSIP_NativeMethods.PortSIP_stopPlayAudioFileToRemoteAsBackground(_LibSDK, sessionId);
        }

        /** @} */
        // end of group12

        /** @defgroup group13 Conference functions
         * @{
         */

        /*!
         *  @brief Create a conference. It's failures if the exists conference isn't destroy yet.
         *
         *  @param conferenceVideoWindow         The UIView which using to display the conference video.
         *  @param videoResolution               The conference video resolution.
         *  @param displayLocalVideoInConference Display the local video on video window or not. 
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 createConference(IntPtr conferenceVideoWindow, VIDEO_RESOLUTION videoResolution, Boolean displayLocalVideoInConference)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_createConference(_LibSDK, conferenceVideoWindow, (Int32)videoResolution, displayLocalVideoInConference);
        }

        /*!
         *  @brief Destroy the exist conference.
         */
        public void destroyConference()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_destroyConference(_LibSDK);
        }

        /*!
         *  @brief Set the window for a conference that using to display the received remote video image.
         *
         *  @param videoWindow The UIView which using to display the conference video.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setConferenceVideoWindow(IntPtr videoWindow)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setConferenceVideoWindow(_LibSDK, videoWindow);
        }

        /*!
         *  @brief Join a session into exist conference, if the call is in hold, it will be un-hold automatically.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 joinToConference(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_joinToConference(_LibSDK, sessionId);
        }

        /*!
         *  @brief Remove a session from an exist conference.
         *
         *  @param sessionId Session ID of the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 removeFromConference(Int32 sessionId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_removeFromConference(_LibSDK, sessionId);
        }

        /** @} */
        // end of group13

        /** @defgroup group14 RTP and RTCP QOS functions
         * @{
         */

        /*!
         *  @brief Set the audio RTCP bandwidth parameters as the RFC3556.
         *
         *  @param sessionId The session ID of call conversation.
         *  @param BitsRR    The bits for the RR parameter.
         *  @param BitsRS    The bits for the RS parameter.
         *  @param KBitsAS   The Kbits for the AS parameter.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setAudioRtcpBandwidth(Int32 sessionId,
                                           Int32 BitsRR,
                                           Int32 BitsRS,
                                           Int32 KBitsAS)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

           return PortSIP_NativeMethods.PortSIP_setAudioRtcpBandwidth(_LibSDK, sessionId, BitsRR, BitsRS, KBitsAS);
        }

        /*!
         *  @brief Set the video RTCP bandwidth parameters as the RFC3556.
         *
         *  @param sessionId The session ID of call conversation.
         *  @param BitsRR    The bits for the RR parameter.
         *  @param BitsRS    The bits for the RS parameter.
         *  @param KBitsAS   The Kbits for the AS parameter.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoRtcpBandwidth(Int32 sessionId,
                                          Int32 BitsRR,
                                          Int32 BitsRS,
                                          Int32 KBitsAS)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

           return PortSIP_NativeMethods.PortSIP_setVideoRtcpBandwidth(_LibSDK, sessionId, BitsRR, BitsRS, KBitsAS);
        }

        /*!
         *  @brief Set the DSCP(differentiated services code point) value of QoS(Quality of Service) for audio channel.
         *
         *  @param state        Set to true to enable audio QoS.
         *  @param DSCPValue    The six-bit DSCP value. Valid range is 0-63. As defined in RFC 2472, the DSCP value is the high-order 6 bits of the IP version 4 (IPv4) TOS field and the IP version 6 (IPv6) Traffic Class field.
         *  @param priority     The 802.1p priority(PCP) field in a 802.1Q/VLAN tag. Values 0-7 set the priority, value -1 leaves the priority setting unchanged.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setAudioQos(Boolean state, Int32 DSCPValue, Int32 priority)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setAudioQos(_LibSDK, state, DSCPValue, priority);
        }

        /*!
         *  @brief Set the DSCP(differentiated services code point) value of QoS(Quality of Service) for video channel.
         *
         *  @param state    Set as true to enable QoS, false to disable.
         *  @param DSCPValue The six-bit DSCP value. Valid range is 0-63. As defined in RFC 2472, the DSCP value is the high-order 6 bits of the IP version 4 (IPv4) TOS field and the IP version 6 (IPv6) Traffic Class field.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setVideoQos(Boolean state, Int32 DSCPValue)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setVideoQos(_LibSDK, state, DSCPValue);
        }

        /** @} */
        // end of group14

        /** @defgroup group15 RTP statistics functions
         * @{
         */

        /*!
         *  @brief Get the "in-call" statistics. The statistics are reset after the query.
         *
         *  @param sessionId             The session ID of call conversation.
         *  @param currentBufferSize     Current jitter buffer size in ms.
         *  @param preferredBufferSize   Preferred (optimal) buffer size in ms.
         *  @param currentPacketLossRate Loss rate (network + late) in percent.
         *  @param currentDiscardRate    Late loss rate in percent.
         *  @param currentExpandRate     Fraction (of original stream) of synthesized speech inserted through expansion.
         *  @param currentPreemptiveRate Fraction of synthesized speech inserted through pre-emptive expansion.
         *  @param currentAccelerateRate Fraction of data removed through acceleration through acceleration.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getNetworkStatistics(Int32 sessionId,
                                         out Int32 currentBufferSize,
                                         out Int32 preferredBufferSize,
                                         out Int32 currentPacketLossRate,
                                         out Int32 currentDiscardRate,
                                         out Int32 currentExpandRate,
                                         out Int32 currentPreemptiveRate,
                                         out Int32 currentAccelerateRate)
        {

            if (_LibSDK == IntPtr.Zero)
            {
                currentBufferSize = 0;
                preferredBufferSize = 0;
                currentPacketLossRate = 0;
                currentDiscardRate = 0;
                currentExpandRate = 0;
                currentPreemptiveRate = 0;
                currentAccelerateRate = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNetworkStatistics(_LibSDK,
                                                sessionId,
                                                out currentBufferSize,
                                                out preferredBufferSize,
                                                out currentPacketLossRate,
                                                out currentDiscardRate,
                                                out currentExpandRate,
                                                out currentPreemptiveRate,
                                                out currentAccelerateRate);
        }

        /*!
         *  @brief Obtain the RTP statisics of audio channel.
         *
         *  @param sessionId        The session ID of call conversation.
         *  @param averageJitterMs  Short-time average jitter (in milliseconds).
         *  @param maxJitterMs      Maximum short-time jitter (in milliseconds).
         *  @param discardedPackets The number of discarded packets on a channel during the call.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getAudioRtpStatistics(Int32 sessionId,
                                           out Int32 averageJitterMs,
                                           out Int32 maxJitterMs,
                                           out Int32 discardedPackets)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                averageJitterMs = 0;
                maxJitterMs = 0;
                discardedPackets = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getAudioRtpStatistics(_LibSDK, 
                                                                       sessionId, 
                                                                       out averageJitterMs, 
                                                                       out maxJitterMs,
                                                                       out discardedPackets);
        }


        /*!
         *  @brief Obtain the RTCP statisics of audio channel.
         *
         *  @param sessionId       		The session ID of call conversation.
         *  @param bytesSent       		The number of sent bytes.
         *  @param packetsSent     		The number of sent packets.
         *  @param bytesReceived   		The number of received bytes.
         *  @param packetsReceived 		The number of received packets.
         *  @param sendFractionLost  	Fraction of sent lost in percent.
         *  @param sendCumulativeLost The number of sent cumulative lost packet.
         *  @param recvFractionLost  	Fraction of received lost in percent.
         *  @param recvCumulativeLost The number of received cumulative lost packets.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getAudioRtcpStatistics(Int32 sessionId,
                                                    out Int32 bytesSent,
                                                    out Int32 packetsSent,
                                                    out Int32 bytesReceived,
                                                    out Int32 packetsReceived,
                                                    out Int32 sendFractionLost,
                                                    out Int32 sendCumulativeLost,
                                                    out Int32 recvFractionLost,
                                                    out Int32 recvCumulativeLost)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                bytesSent = 0;
                packetsSent = 0;
                bytesReceived = 0;
                packetsReceived = 0;
                sendFractionLost = 0;
                sendCumulativeLost = 0;
                recvFractionLost = 0;
                recvCumulativeLost = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getAudioRtcpStatistics(_LibSDK,
                                                                       sessionId,
                                                                       out bytesSent,
                                                                       out packetsSent,
                                                                       out bytesReceived,
                                                                       out packetsReceived,
                                                                       out sendFractionLost,
                                                                       out sendCumulativeLost,
                                                                       out recvFractionLost,
                                                                       out recvCumulativeLost);
        }

        /*!
         *  @brief Obtain the RTP statisics of video.
         *
         *  @param sessionId       The session ID of call conversation.
         *  @param bytesSent       The number of sent bytes.
         *  @param packetsSent     The number of sent packets.
         *  @param bytesReceived   The number of received bytes.
         *  @param packetsReceived The number of received packets.
         *
         *  @return  If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getVideoRtpStatistics(Int32 sessionId, 
                                            out Int32 bytesSent,
                                            out Int32 packetsSent,
                                            out Int32 bytesReceived,
                                            out Int32 packetsReceived)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                bytesSent = 0;
                packetsSent = 0;
                bytesReceived = 0;
                packetsReceived = 0;

                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getVideoRtpStatistics(_LibSDK,
                                                                       sessionId,
                                                                       out bytesSent,
                                                                       out packetsSent,
                                                                       out bytesReceived,
                                                                       out packetsReceived);
        }

        /** @} */
        // end of group15

        /** @defgroup group16 Audio effect functions
         * @{
         */

        /*!
         *  @brief Enable/disable Voice Activity Detection(VAD).
         *
         *  @param state Set to true to enable VAD, false to disable.
         */
        public void enableVAD(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableVAD(_LibSDK, state);
        }

        /*!
         *  @brief Enable/disable AEC (Acoustic Echo Cancellation).
         *
         *  @param ecMode Allow set the AEC mode to effect for different scenarios.
         *  
         * <p><table>
         * <tr><th>Mode</th><th>Description</th></tr>
         * <tr><td>EC_NONE</td><td>Disable AEC.  </td></tr>
         * <tr><td>EC_DEFAULT</td><td>Platform default AEC. </td></tr>
         * <tr><td>EC_CONFERENCE</td><td>Desktop platform(windows,MAC) Conferencing default (aggressive AEC). </td></tr>
         * </table></p>
         * 
         */
        public void enableAEC(EC_MODES ecMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableAEC(_LibSDK, (Int32)ecMode);
        }

        /*!
         *  @brief Enable/disable Comfort Noise Generator(CNG).
         *
         *  @param state state Set to true to enable CNG, false to disable.
         */
        public void enableCNG(Boolean state)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableCNG(_LibSDK, state);
        }

        /*!
         *  @brief Enable/disable Automatic Gain Control(AGC).
         *
         *  @param agcMode  Allow set the AGC mode to effect for different scenarios.
         *  
         * <p><table>
         * <tr><th>Mode</th><th>Description</th></tr>
         * <tr><td>AGC_DEFAULT</td><td>Disable AGC.  </td></tr>
         * <tr><td>AGC_DEFAULT</td><td>Platform default. </td></tr>
         * <tr><td>AGC_ADAPTIVE_ANALOG</td><td>Desktop platform(windows,MAC) adaptive mode for use when analog volume control exists. </td></tr>
         * <tr><td>AGC_ADAPTIVE_DIGITAL</td><td>scaling takes place in the digital domain (e.g. for conference servers and embedded devices). </td></tr>
         * <tr><td>AGC_FIXED_DIGITAL</td><td>can be used on embedded devices where the capture signal level is predictable. </td></tr>
         * </table></p>
         */
        public void enableAGC(AGC_MODES agcMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableAGC(_LibSDK, (Int32)agcMode);
        }

        /*!
         *  @brief Enable/disable Audio Noise Suppression(ANS).
         *
         *  @param nsMode Allow set the NS mode to effect for different scenarios.
         *  
         * <p><table>
         * <tr><th>Mode                     </th><th>Description</th></tr>
         * <tr><td>NS_NONE                  </td><td>Disable NS.  </td></tr>
         * <tr><td>NS_DEFAULT               </td><td>Platform default.  </td></tr>
         * <tr><td>NS_Conference            </td><td>conferencing default. </td></tr>
         * <tr><td>NS_LOW_SUPPRESSION       </td><td>lowest suppression. </td></tr>
         * <tr><td>NS_MODERATE_SUPPRESSION  </td><td>moderate suppression. </td></tr>
         * <tr><td>NS_HIGH_SUPPRESSION      </td><td>high suppression </td></tr>
         * <tr><td>NS_VERY_HIGH_SUPPRESSION </td><td>highest suppression. </td></tr>
         * </table></p>
         */
        public void enableANS(NS_MODES nsMode)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_enableANS(_LibSDK, (Int32)nsMode);
        }

        /** @} */
        // end of group16

        /** @defgroup group17 Send OPTIONS/INFO/MESSAGE functions
         * @{
         */

        /*!
         *  @brief Send OPTIONS message.
         *
         *  @param to  The receiver of OPTIONS message.
         *  @param sdp The SDP of OPTIONS message, it's optional if don't want send the SDP with OPTIONS message.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 sendOptions(String to, String sdp)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendOptions(_LibSDK, to, sdp);
        }

        /*!
         *  @brief Send a INFO message to remote side in dialog.
         *
         *  @param sessionId    The session ID of call.
         *  @param mimeType     The mime type of INFO message.
         *  @param subMimeType  The sub mime type of INFO message.
         *  @param infoContents The contents that send with INFO message.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 sendInfo(Int32 sessionId, String mimeType, String subMimeType, String infoContents)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendInfo(_LibSDK, sessionId, mimeType, subMimeType, infoContents);
        }

        /*!
         *  @brief Send a MESSAGE message to remote side in dialog.
         *
         *  @param sessionId     The session ID of the call.
         *  @param mimeType      The mime type of MESSAGE message.
         *  @param subMimeType   The sub mime type of MESSAGE message.
         *  @param message       The contents which send with MESSAGE message, allow binary data.
         *  @param messageLength The message size.
         *
         *  @return If the function succeeds, the return value is a message ID allows track the message send state in onSendMessageSuccess and onSendMessageFailure. If the function fails, the return value is a specific error code less than 0.
         *  @remark  Example 1: send a plain text message. Note: to send other languages text, please use the UTF8 to encode the message before send.
         @code
         sendMessage(sessionId, "text", "plain", "hello",6);
         @endcode
         Example 2: send a binary message.
         @code
         sendMessage(sessionId, "application", "vnd.3gpp.sms", binData, binDataSize);
         @endcode
         */
        public Int32 sendMessage(Int32 sessionId, String mimeType, String subMimeType, byte[] message, Int32 messageLength)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendMessage(_LibSDK, sessionId, mimeType, subMimeType, message, messageLength);
        }

        /*!
         *  @brief Send a out of dialog MESSAGE message to remote side.
         *
         *  @param to            The message receiver. Likes sip:receiver@portsip.com
         *  @param mimeType      The mime type of MESSAGE message.
         *  @param subMimeType   The sub mime type of MESSAGE message.
         *  @param message       The contents which send with MESSAGE message, allow binary data.
         *  @param messageLength The message size.
         *
         *  @return If the function succeeds, the return value is a message ID allows track the message send state in onSendOutOfMessageSuccess and onSendOutOfMessageFailure.  If the function fails, the return value is a specific error code less than 0.
         *  @remark
         *  Example 1: send a plain text message. Note: to send other languages text, please use the UTF8 to encode the message before send.
         *  @code
            sendOutOfDialogMessage("sip:user1@sip.portsip.com", "text", "plain", "hello", 6);
         *  @endcode
         Example 2: send a binary message.
         *  @code
           sendOutOfDialogMessage("sip:user1@sip.portsip.com","application",  "vnd.3gpp.sms", binData, binDataSize);
         @endcode
         */
        public Int32 sendOutOfDialogMessage(String to, String mimeType, String subMimeType, byte[] message, Int32 messageLength)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_sendOutOfDialogMessage(_LibSDK, to, mimeType, subMimeType, message, messageLength);
        }

        /** @} */
        // end of group17

        /** @defgroup group18 Presence functions
         * @{
         */

        /*!
         *  @brief Send a SUBSCRIBE message for presence to a contact.
         *
         *  @param contact The target contact, it must likes sip:contact001@sip.portsip.com.
         *  @param subject This subject text will be insert into the SUBSCRIBE message. For example: "Hello, I'm Jason".<br>
         The subject maybe is UTF8 format, you should use UTF8 to decode it.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 presenceSubscribeContact(String contact, String subject)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceSubscribeContact(_LibSDK, contact, subject);
        }

        /*!
         *  @brief Accept the presence SUBSCRIBE request which received from contact.
         *
         *  @param subscribeId Subscribe id, when received a SUBSCRIBE request from contact, the event onPresenceRecvSubscribe will be triggered,the event inclues the subscribe id.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 presenceRejectSubscribe(Int32 subscribeId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceRejectSubscribe(_LibSDK, subscribeId);
        }

        /*!
         *  @brief Reject a presence SUBSCRIBE request which received from contact.
         *
         *  @param subscribeId Subscribe id, when received a SUBSCRIBE request from contact, the event onPresenceRecvSubscribe will be triggered,the event inclues the subscribe id.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 presenceAcceptSubscribe(Int32 subscribeId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceAcceptSubscribe(_LibSDK, subscribeId);
        }

        /*!
         *  @brief Send a NOTIFY message to contact to notify that presence status is online/changed.
         *
         *  @param subscribeId Subscribe id, when received a SUBSCRIBE request from contact, the event onPresenceRecvSubscribe will be triggered,the event inclues the subscribe id.
         *  @param stateText   The state text of presende online, for example: "I'm here"
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 presenceOnline(Int32 subscribeId, String stateText)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceOnline(_LibSDK, subscribeId, stateText);
        }

        /*!
         *  @brief Send a NOTIFY message to contact to notify that presence status is offline.
         *
         *  @param subscribeId Subscribe id, when received a SUBSCRIBE request from contact, the event onPresenceRecvSubscribe will be triggered,the event inclues the subscribe id.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 presenceOffline(Int32 subscribeId)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_presenceOffline(_LibSDK, subscribeId);
        }

        /** @} */
        // end of group18

        /** @defgroup group19 Device Manage functions.
         * @{
         */

        /*!
         *  @brief Gets the number of audio devices available for audio recording
         *
         *  @return The return value is number of recording devices. If the function fails, the return value is a specific error code less than 0.
         */
        public Int32 getNumOfRecordingDevices()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfRecordingDevices(_LibSDK);
        }

        /*!
         *  @brief Gets the number of audio devices available for audio playout
         *
         *  @return The return value is number of playout devices. If the function fails, the return value is a specific error code less than 0.
         */
        public Int32 getNumOfPlayoutDevices()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfPlayoutDevices(_LibSDK);
        }

        /*!
         *  @brief Gets the name of a specific recording device given by an index.
         *
         *  @param deviceIndex Device index (0, 1, 2, ..., N-1), where N is given by getNumOfRecordingDevices (). Also -1 is a valid value and will return the name of the default recording device.
         *  @param nameUTF8 A character buffer to which the device name will be copied as a null-terminated string in UTF8 format. 
         *  @param nameUTF8Length The size of nameUTF8 buffer, don't let it less than 128.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code. 
         */
        public Int32 getRecordingDeviceName(Int32 deviceIndex, StringBuilder nameUTF8, Int32 nameUTF8Length)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getRecordingDeviceName(_LibSDK, deviceIndex, nameUTF8, nameUTF8Length);
        }

        /*!
         *  @brief Gets the name of a specific playout device given by an index
         *
         *  @param deviceIndex 
         *  @param deviceIndex Device index (0, 1, 2, ..., N-1), where N is given by getNumOfRecordingDevices (). Also -1 is a valid value and will return the name of the default recording device.
         *  @param nameUTF8 A character buffer to which the device name will be copied as a null-terminated string in UTF8 format. 
         *  @param nameUTF8Length The size of nameUTF8 buffer, don't let it less than 128.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code. 
         */
        public Int32 getPlayoutDeviceName(Int32 deviceIndex, StringBuilder nameUTF8, Int32 nameUTF8Length)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getPlayoutDeviceName(_LibSDK, deviceIndex, nameUTF8, nameUTF8Length);
        }

        /*!
         *  @brief Set the speaker volume level,
         *
         *  @param volume Volume level of speaker, valid range is 0 - 255.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setSpeakerVolume(Int32 volume)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSpeakerVolume(_LibSDK, volume);
        }

        /*!
         *  @brief Gets the speaker volume level
         *
         *  @return If the function succeeds, the return value is speaker volume, valid range is 0 - 255. If the function fails, the return value is a specific error code.
         */
        public Int32 getSpeakerVolume()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getSpeakerVolume(_LibSDK);
        }

        /*!
         *  @brief Mutes the speaker device completely in the OS
         *
         *  @param enable If set to true, the device output is muted. If set to false, the output is unmuted.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setSystemOutputMute(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSystemOutputMute(_LibSDK, enable);
        }

        /*!
         *  @brief Retrieves the output device mute state in the operating system
         *
         *  @return If return value is true, the output device is muted. If false, the output device is not muted.
         */
        public Boolean getSystemOutputMute()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return false;
            }

            return PortSIP_NativeMethods.PortSIP_getSystemOutputMute(_LibSDK);
        }

        /*!
         *  @brief Sets the microphone volume level.
         *
         *  @param volume The microphone volume level, the valid value is 0 - 255.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setMicVolume(Int32 volume)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setMicVolume(_LibSDK, volume);
        }

        /*!
         *  @brief Retrieves the current microphone volume.
         *
         *  @return If the function succeeds, the return value is the microphone volume. If the function fails, the return value is a specific error code.
         */
        public Int32 getMicVolume()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getMicVolume(_LibSDK);
        }

        /*!
         *  @brief Mute the microphone input device completely in the OS
         *
         *  @param enable If set to true, the input device is muted. Set to false is unmuted.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 setSystemInputMute(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_setSystemInputMute(_LibSDK, enable);
        }

        /*!
         *  @brief Gets the mute state of the input device in the operating system
         *
         *  @return If return value is true, the input device is muted. If false, the input device is not muted.
         */
        public  Boolean getSystemInputMute()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return false;
            }

            return PortSIP_NativeMethods.PortSIP_getSystemInputMute(_LibSDK);
        }

        /*!
         *  @brief Use to do the audio device loop back test
         *
         *  @param enable Set to true start audio look back test; Set to fase to stop.
         */
        public void audioPlayLoopbackTest(Boolean enable)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return;
            }

            PortSIP_NativeMethods.PortSIP_audioPlayLoopbackTest(_LibSDK, enable);
        }

        /*!
         *  @brief Gets the number of available capture devices.
         *
         *  @return The return value is number of video capture devices, if fails the return value is a specific error code less than 0.
         */
        public Int32 getNumOfVideoCaptureDevices()
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getNumOfVideoCaptureDevices(_LibSDK);
        }

        /*!
         *  @brief Gets the name of a specific video capture device given by an index
         *
         *  @param deviceIndex          Device index (0, 1, 2, ..., N-1), where N is given by getNumOfVideoCaptureDevices (). Also -1 is a valid value and will return the name of the default capture device.
         *  @param uniqueIdUTF8   Unique identifier of the capture device.
         *  @param uniqueIdUTF8Length Size in bytes of uniqueIdUTF8. 
         *  @param deviceNameUTF8 A character buffer to which the device name will be copied as a null-terminated string in UTF8 format.
         *  @param deviceNameUTF8Length The size of nameUTF8 buffer, don't let it less than 128.
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 getVideoCaptureDeviceName(Int32 deviceIndex,
                                               StringBuilder uniqueIdUTF8,
                                               Int32 uniqueIdUTF8Length,
                                               StringBuilder deviceNameUTF8,
                                               Int32 deviceNameUTF8Length)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_getVideoCaptureDeviceName(_LibSDK,
                                                                           deviceIndex,
                                                                           uniqueIdUTF8,
                                                                           uniqueIdUTF8Length,
                                                                           deviceNameUTF8,
                                                                           deviceNameUTF8Length);
        }

        /*!
         *  @brief Display the capture device property dialog box for the specified capture device.
         *
         *  @param uniqueIdUTF8     Unique identifier of the capture device. 
         *  @param uniqueIdUTF8Length Size in bytes of uniqueIdUTF8. 
         *  @param dialogTitle      The title of the video settings dialog. 
         *  @param parentWindow     Parent window to use for the dialog box, should originally be a HWND. 
         *  @param x                Horizontal position for the dialog relative to the parent window, in pixels. 
         *  @param y                Vertical position for the dialog relative to the parent window, in pixels. 
         *
         *  @return If the function succeeds, the return value is 0. If the function fails, the return value is a specific error code.
         */
        public Int32 showVideoCaptureSettingsDialogBox(String uniqueIdUTF8,
                                                                    Int32 uniqueIdUTF8Length,
                                                                    String dialogTitle,
                                                                    IntPtr parentWindow,
                                                                    Int32 x,
                                                                    Int32 y)
        {
            if (_LibSDK == IntPtr.Zero)
            {
                return PortSIP_Errors.ECoreSDKObjectNull;
            }

            return PortSIP_NativeMethods.PortSIP_showVideoCaptureSettingsDialogBox(_LibSDK,
                                                                                    uniqueIdUTF8,
                                                                                    uniqueIdUTF8Length,
                                                                                    dialogTitle,
                                                                                    parentWindow,
                                                                                    x,
                                                                                    y);
        }
        /** @} */ // end of group19
        /** @} */ // end of groupSDK
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Private members and methods
        /// </summary>


        private  PortSIP_NativeMethods.registerSuccess _rgs;
        private  PortSIP_NativeMethods.registerFailure _rgf;
        private  PortSIP_NativeMethods.inviteIncoming _ivi;
        private  PortSIP_NativeMethods.inviteTrying _ivt;
        private  PortSIP_NativeMethods.inviteSessionProgress _ivsp;
        private  PortSIP_NativeMethods.inviteRinging _ivr;
        private  PortSIP_NativeMethods.inviteAnswered _iva;
        private  PortSIP_NativeMethods.inviteFailure _ivf;
        private  PortSIP_NativeMethods.inviteUpdated _ivu;
        private  PortSIP_NativeMethods.inviteConnected _ivc;
        private  PortSIP_NativeMethods.inviteBeginingForward _ivbf;
        private  PortSIP_NativeMethods.inviteClosed _ivcl;
        private  PortSIP_NativeMethods.remoteHold _rmh;
        private  PortSIP_NativeMethods.remoteUnHold _rmuh;
        private  PortSIP_NativeMethods.receivedRefer _rvr;
        private  PortSIP_NativeMethods.referAccepted _rfa;
        private  PortSIP_NativeMethods.referRejected _rfr;
        private  PortSIP_NativeMethods.transferTrying _tft;
        private  PortSIP_NativeMethods.transferRinging _tfr;
        private  PortSIP_NativeMethods.ACTVTransferSuccess _ats;
        private  PortSIP_NativeMethods.ACTVTransferFailure _atf;
        private  PortSIP_NativeMethods.receivedSignaling _rvs;
        private  PortSIP_NativeMethods.sendingSignaling _sds;
        private  PortSIP_NativeMethods.waitingVoiceMessage _wvm;
        private  PortSIP_NativeMethods.waitingFaxMessage _wfm;
        private  PortSIP_NativeMethods.recvDtmfTone _rdt;
        private  PortSIP_NativeMethods.presenceRecvSubscribe _prs;
        private  PortSIP_NativeMethods.presenceOnline _pon;
        private  PortSIP_NativeMethods.presenceOffline _pof;
        private  PortSIP_NativeMethods.recvOptions _rvo;
        private  PortSIP_NativeMethods.recvInfo _rvi;
        private  PortSIP_NativeMethods.recvMessage _rvm;
        private  PortSIP_NativeMethods.recvOutOfDialogMessage _rodm;
        private  PortSIP_NativeMethods.sendMessageSuccess _sms;
        private  PortSIP_NativeMethods.sendMessageFailure _smf;
        private  PortSIP_NativeMethods.sendOutOfDialogMessageSuccess _sdms;
        private  PortSIP_NativeMethods.sendOutOfDialogMessageFailure _sdmf;
        private  PortSIP_NativeMethods.playAudioFileFinished _paf;
        private PortSIP_NativeMethods.playVideoFileFinished _pvf;
        private  PortSIP_NativeMethods.audioRawCallback _arc; 
        private  PortSIP_NativeMethods.videoRawCallback _vrc;
        private  PortSIP_NativeMethods.receivedRTPCallback _rrc;
        private PortSIP_NativeMethods.sendingRTPCallback _src;


        private void initializeCallbackFunctions()
        {

            _arc = new PortSIP_NativeMethods.audioRawCallback(onAudioRawCallback);
            _vrc = new PortSIP_NativeMethods.videoRawCallback(onVideoRawCallback);
            _rrc = new PortSIP_NativeMethods.receivedRTPCallback(onReceivedRtpPacket);
            _src = new PortSIP_NativeMethods.sendingRTPCallback(onSendingRtpPacket);


            _paf = new PortSIP_NativeMethods.playAudioFileFinished(onPlayAudioFileFinished);
            _pvf = new PortSIP_NativeMethods.playVideoFileFinished(onPlayVideoFileFinished);

            _rgs = new PortSIP_NativeMethods.registerSuccess(onRegisterSuccess);
            _rgf = new PortSIP_NativeMethods.registerFailure(onRegisterFailure);
            _ivi = new PortSIP_NativeMethods.inviteIncoming(onInviteIncoming);
            _ivt = new PortSIP_NativeMethods.inviteTrying(onInviteTrying);
            _ivsp = new PortSIP_NativeMethods.inviteSessionProgress(onInviteSessionProgress);
            _ivr = new PortSIP_NativeMethods.inviteRinging(onInviteRinging);
            _iva = new PortSIP_NativeMethods.inviteAnswered(onInviteAnswered);
            _ivf = new PortSIP_NativeMethods.inviteFailure(onInviteFailure);
            _ivu = new PortSIP_NativeMethods.inviteUpdated(onInviteUpdated);
            _ivc = new PortSIP_NativeMethods.inviteConnected(onInviteConnected);
            _ivbf = new PortSIP_NativeMethods.inviteBeginingForward(onInviteBeginingForward);
            _ivcl = new PortSIP_NativeMethods.inviteClosed(onInviteClosed);
            _rmh = new PortSIP_NativeMethods.remoteHold(onRemoteHold);
            _rmuh = new PortSIP_NativeMethods.remoteUnHold(onRemoteUnHold);
            _rvr = new PortSIP_NativeMethods.receivedRefer(onReceivedRefer);
            _rfa = new PortSIP_NativeMethods.referAccepted(onReferAccepted);
            _rfr = new PortSIP_NativeMethods.referRejected(onReferRejected);
            _tft = new PortSIP_NativeMethods.transferTrying(onTransferTrying);
            _tfr = new PortSIP_NativeMethods.transferRinging(onTransferRinging);
            _ats = new PortSIP_NativeMethods.ACTVTransferSuccess(onACTVTransferSuccess);
            _atf = new PortSIP_NativeMethods.ACTVTransferFailure(onACTVTransferFailure);
            _rvs = new PortSIP_NativeMethods.receivedSignaling(onReceivedSignaling);
            _sds = new PortSIP_NativeMethods.sendingSignaling(onSendingSignaling);
            _wvm = new PortSIP_NativeMethods.waitingVoiceMessage(onWaitingVoiceMessage);
            _wfm = new PortSIP_NativeMethods.waitingFaxMessage(onWaitingFaxMessage);
            _rdt = new PortSIP_NativeMethods.recvDtmfTone(onRecvDtmfTone);
            _prs = new PortSIP_NativeMethods.presenceRecvSubscribe(onPresenceRecvSubscribe);
            _pon = new PortSIP_NativeMethods.presenceOnline(onPresenceOnline);
            _pof = new PortSIP_NativeMethods.presenceOffline(onPresenceOffline);
            _rvo = new PortSIP_NativeMethods.recvOptions(onRecvOptions);
            _rvi = new PortSIP_NativeMethods.recvInfo(onRecvInfo);
            _rvm = new PortSIP_NativeMethods.recvMessage(onRecvMessage);
            _rodm = new PortSIP_NativeMethods.recvOutOfDialogMessage(onRecvOutOfDialogMessage);
            _sms = new PortSIP_NativeMethods.sendMessageSuccess(onSendMessageSuccess);
            _smf = new PortSIP_NativeMethods.sendMessageFailure(onSendMessageFailure);
            _sdms = new PortSIP_NativeMethods.sendOutOfDialogMessageSuccess(onSendOutOfDialogMessageSuccess);
            _sdmf = new PortSIP_NativeMethods.sendOutOfDialogMessageFailure(onSendOutOfDialogMessageFailure);           

        }

        private unsafe Int32 onRegisterSuccess(Int32 callbackIndex, Int32 callbackObject, String statusText, Int32 statusCode)
        {
            _SIPCallbackEvents.onRegisterSuccess(callbackIndex, callbackObject, statusText, statusCode);
            return 0;
        }

        private unsafe Int32 onRegisterFailure(Int32 callbackIndex, Int32 callbackObject, String statusText, Int32 statusCode)
        {
            _SIPCallbackEvents.onRegisterFailure(callbackIndex, callbackObject, statusText, statusCode);
            return 0;
        }

       
        private unsafe Int32 onInviteIncoming(Int32 callbackIndex,
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
            _SIPCallbackEvents.onInviteIncoming(callbackIndex, 
                                                callbackObject, 
                                                sessionId, 
                                                callerDisplayName,
                                                caller,
                                                calleeDisplayName,
                                                callee,
                                                audioCodecNames,
                                                videoCodecNames,
                                                existsAudio,
                                                existsVideo);
            return 0;
        }

        private unsafe Int32 onInviteTrying(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onInviteTrying(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onInviteSessionProgress(Int32 callbackIndex,
                                            Int32 callbackObject,
                                            Int32 sessionId,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsEarlyMedia,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            _SIPCallbackEvents.onInviteSessionProgress(_callbackIndex,
                                                        _callbackObject,
                                                        sessionId,
                                                        audioCodecNames,
                                                        videoCodecNames,
                                                        existsEarlyMedia,
                                                        existsAudio,
                                                        existsVideo);

            return 0;
        }

        private unsafe Int32 onInviteRinging(Int32 callbackIndex,
                                            Int32 callbackObject,
                                            Int32 sessionId,
                                            String statusText,
                                            Int32 statusCode)
        {
            _SIPCallbackEvents.onInviteRinging(_callbackIndex, _callbackObject, sessionId, statusText, statusCode);

            return 0;
        }

        private unsafe Int32 onInviteAnswered(Int32 callbackIndex,
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
            _SIPCallbackEvents.onInviteAnswered(_callbackIndex,
                                                _callbackObject,
                                                sessionId,
                                                callerDisplayName,
                                                caller,
                                                calleeDisplayName,
                                                callee,
                                                audioCodecNames,
                                                videoCodecNames, 
                                                existsAudio,
                                                existsVideo);

            return 0;
        }

        private unsafe Int32 onInviteFailure(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            _SIPCallbackEvents.onInviteFailure(_callbackIndex, _callbackObject, sessionId, reason, code);
            return 0;
        }

        private unsafe Int32 onInviteUpdated(Int32 callbackIndex,
                                             Int32 callbackObject,
                                             Int32 sessionId,
                                             String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            _SIPCallbackEvents.onInviteUpdated(_callbackIndex, _callbackObject, sessionId, audioCodecNames, videoCodecNames, existsAudio, existsVideo);
            return 0;
        }

        private unsafe Int32 onInviteConnected(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onInviteConnected(_callbackIndex, _callbackObject, sessionId);

            return 0;
                 
        }

        private unsafe Int32 onInviteBeginingForward(Int32 callbackIndex, Int32 callbackObject, String forwardTo)
        {
            _SIPCallbackEvents.onInviteBeginingForward(_callbackIndex, _callbackObject, forwardTo);
            return 0;
        }

        private unsafe Int32 onInviteClosed(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onInviteClosed(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onRemoteHold(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onRemoteHold(_callbackIndex, _callbackObject, sessionId);

            return 0;
              
        }

        private unsafe Int32 onRemoteUnHold(Int32 callbackIndex, 
                                            Int32 callbackObject, 
                                            Int32 sessionId,
                                            String audioCodecNames,
                                             String videoCodecNames,
                                             Boolean existsAudio,
                                             Boolean existsVideo)
        {
            _SIPCallbackEvents.onRemoteUnHold(_callbackIndex, 
                                                _callbackObject, 
                                                sessionId,
                                                audioCodecNames,
                                                videoCodecNames,
                                                existsAudio,
                                                existsVideo);
            return 0;
        }

        private unsafe Int32 onReceivedRefer(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    Int32 sessionId,
                                                    Int32 referId,
                                                    String to,
                                                    String from,
                                                    String referSipMessage)
        {
            _SIPCallbackEvents.onReceivedRefer(_callbackIndex, _callbackObject, sessionId, referId, to, from, referSipMessage);
            return 0;
        }

        private unsafe Int32 onReferAccepted(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onReferAccepted(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onReferRejected(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            _SIPCallbackEvents.onReferRejected(_callbackIndex, _callbackObject, sessionId, reason, code);
            return 0;
        }

        private unsafe Int32 onTransferTrying(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onTransferTrying(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onTransferRinging(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onTransferRinging(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onACTVTransferSuccess(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onACTVTransferSuccess(_callbackIndex, _callbackObject, sessionId);
            return 0;
        }

        private unsafe Int32 onACTVTransferFailure(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String reason, Int32 code)
        {
            _SIPCallbackEvents.onACTVTransferFailure(_callbackIndex, _callbackObject, sessionId, reason, code);
            return 0;
        }

        private unsafe Int32 onReceivedSignaling(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            _SIPCallbackEvents.onReceivedSignaling(_callbackIndex, _callbackObject, sessionId, signaling);
            return 0;
        }

        private unsafe Int32 onSendingSignaling(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            _SIPCallbackEvents.onSendingSignaling(_callbackIndex, _callbackObject, sessionId, signaling);
            return 0;
        }

        private unsafe Int32 onWaitingVoiceMessage(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            _SIPCallbackEvents.onWaitingVoiceMessage(_callbackIndex,
                                                    _callbackObject,
                                                    messageAccount,
                                                    urgentNewMessageCount,
                                                    urgentOldMessageCount,
                                                    newMessageCount,
                                                    oldMessageCount);
            return 0;
        }

        private unsafe Int32 onWaitingFaxMessage(Int32 callbackIndex,
                                                       Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            _SIPCallbackEvents.onWaitingFaxMessage(_callbackIndex,
                                        _callbackObject,
                                        messageAccount,
                                        urgentNewMessageCount,
                                        urgentOldMessageCount,
                                        newMessageCount,
                                        oldMessageCount);
            return 0;
        }

        private unsafe Int32 onRecvDtmfTone(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, Int32 tone)
        {
            _SIPCallbackEvents.onRecvDtmfTone(_callbackIndex, _callbackObject, sessionId, tone);
            return 0;
        }

        private unsafe Int32 onRecvOptions(Int32 callbackIndex, Int32 callbackObject, StringBuilder optionsMessage)
        {
            _SIPCallbackEvents.onRecvOptions(_callbackIndex, _callbackObject, optionsMessage);
            return 0;
        }

        private unsafe Int32 onRecvInfo(Int32 callbackIndex, Int32 callbackObject, StringBuilder infoMessage)
        {
            _SIPCallbackEvents.onRecvInfo(_callbackIndex, _callbackObject, infoMessage);
            return 0;
        }

        /*!
         *  This event will be triggered when received the SUBSCRIBE request from a contact.
         *
         *  @param subscribeId     The id of SUBSCRIBE request.
         *  @param fromDisplayName The display name of contact.
         *  @param from            The contact who send the SUBSCRIBE request.
         *  @param subject         The subject of the SUBSCRIBE request.
         */
        private unsafe Int32 onPresenceRecvSubscribe(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String fromDisplayName,
                                                    String from,
                                                    String subject)
        {
            _SIPCallbackEvents.onPresenceRecvSubscribe(_callbackIndex, 
                                                        _callbackObject, 
                                                    subscribeId,
                                                    fromDisplayName,
                                                    from,
                                                    subject);
            return 0;
        }

        private unsafe Int32 onPresenceOnline(Int32 callbackIndex,
                                                    Int32 callbackObject,
                                                    String fromDisplayName,
                                                    String from,
                                                    String stateText)
        {
            _SIPCallbackEvents.onPresenceOnline(_callbackIndex,
                                                    _callbackObject,
                                                    fromDisplayName,
                                                    from,
                                                    stateText);
            return 0;
        }

        private unsafe Int32 onPresenceOffline(Int32 callbackIndex, Int32 callbackObject, String fromDisplayName, String from)
        {
            _SIPCallbackEvents.onPresenceOffline(_callbackIndex,
                                                    _callbackObject,
                                                    fromDisplayName,
                                                    from);
            return 0;
        }

        private unsafe Int32 onRecvMessage(Int32 callbackIndex,
                                                 Int32 callbackObject,
                                                 Int32 sessionId,
                                                 String mimeType,
                                                 String subMimeType,
                                                 byte[] messageData,
                                                 Int32 messageDataLength)
        {
            _SIPCallbackEvents.onRecvMessage(_callbackIndex, 
                                                _callbackObject,  
                                                sessionId,
                                                 mimeType,
                                                 subMimeType,
                                                 messageData,
                                                 messageDataLength);
            return 0;
        }

        private unsafe Int32 onRecvOutOfDialogMessage(Int32 callbackIndex,
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
            _SIPCallbackEvents.onRecvOutOfDialogMessage(_callbackIndex,
                                                _callbackObject,
                                                fromDisplayName,
                                                 from,
                                                 toDisplayName,
                                                 to,
                                                 mimeType,
                                                 subMimeType,
                                                 messageData,
                                                 messageDataLength);
            return 0;
        }

        private unsafe Int32 onSendMessageSuccess(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 sessionId,
                                                        Int32 messageId)
        {
            _SIPCallbackEvents.onSendMessageSuccess(_callbackIndex, _callbackObject, sessionId, messageId);
            return 0;
        }

        private unsafe Int32 onSendMessageFailure(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 sessionId,
                                                        Int32 messageId,
                                                        String reason,
                                                        Int32 code)
        {
            _SIPCallbackEvents.onSendMessageFailure(_callbackIndex,
                                    _callbackObject,
                                    sessionId,
                                    messageId,
                                    reason,
                                    code);
            return 0;
        }

        private unsafe Int32 onSendOutOfDialogMessageSuccess(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 messageId,
                                                        String fromDisplayName,
                                                        String from,
                                                        String toDisplayName,
                                                        String to)
        {
            _SIPCallbackEvents.onSendOutOfDialogMessageSuccess(_callbackIndex,
                                     _callbackObject,
                                     messageId,
                                     fromDisplayName,
                                     from,
                                     toDisplayName,
                                     to);
            return 0;
        }

        private unsafe Int32 onSendOutOfDialogMessageFailure(Int32 callbackIndex,
                                                        Int32 callbackObject,
                                                        Int32 messageId,
                                                        String fromDisplayName,
                                                        String from,
                                                        String toDisplayName,
                                                        String to,
                                                        String reason,
                                                        Int32 code)
        {
            _SIPCallbackEvents.onSendOutOfDialogMessageFailure(_callbackIndex,
                                     _callbackObject,
                                     messageId,
                                     fromDisplayName,
                                     from,
                                     toDisplayName,
                                     to,
                                     reason,
                                     code);
            return 0;
        }

        private unsafe Int32 onPlayAudioFileFinished(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId, String fileName)
        {
            _SIPCallbackEvents.onPlayAudioFileFinished(_callbackIndex, _callbackObject, sessionId, fileName);
            return 0;
        }

        private unsafe Int32 onPlayVideoFileFinished(Int32 callbackIndex, Int32 callbackObject, Int32 sessionId)
        {
            _SIPCallbackEvents.onPlayVideoFileFinished(_callbackIndex, _callbackObject, sessionId);

            return 0;
        }

        private unsafe Int32 onReceivedRtpPacket(IntPtr callbackObject,
                                       Int32 sessionId,
                                       [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                       [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                       Int32 packetSize)
        {
            _SIPCallbackEvents.onReceivedRtpPacket(callbackObject,
                                         sessionId,
                                         isAudio,
                                         RTPPacket,
                                         packetSize);


            return 0;
        }

        private unsafe Int32 onSendingRtpPacket(IntPtr callbackObject,
                                               Int32 sessionId,
                                               [MarshalAs(UnmanagedType.I1)] Boolean isAudio,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] RTPPacket,
                                               Int32 packetSize)
        {
            _SIPCallbackEvents.onSendingRtpPacket(callbackObject,
                                          sessionId,
                                          isAudio,
                                          RTPPacket,
                                          packetSize);


            return 0;
        }

        private unsafe  Int32 onAudioRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] data,
                                               Int32 dataLength,
                                               Int32 samplingFreqHz)
        {
            _SIPCallbackEvents.onAudioRawCallback(callbackObject,
                                         sessionId,
                                         callbackType,
                                         data,
                                         dataLength,
                                         samplingFreqHz);
            return 0;
        }

        private unsafe  Int32 onVideoRawCallback(IntPtr callbackObject,
                                               Int32 sessionId,
                                               Int32 callbackType,
                                               Int32 width,
                                               Int32 height,
                                               [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] data,
                                               Int32 dataLength)
        {
            _SIPCallbackEvents.onVideoRawCallback(callbackObject,
                                         sessionId,
                                         callbackType,
                                         width,
                                         height,
                                         data,
                                         dataLength);

            return 0;

        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private IntPtr _callbackDispatcher;
        private Int32 _callbackObject;
        private IntPtr _LibSDK;
        private Int32 _callbackIndex;
    }
}
