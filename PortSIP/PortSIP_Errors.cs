
//////////////////////////////////////////////////////////////////////////
//
// IMPORTANT: DON'T EDIT THIS FILE
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace PortSIP
{
    public class PortSIP_Errors
    {
        public static readonly int INVALID_SESSION_ID = -1;


        public static readonly int ECoreAlreadyInitialized				= -60000;
        public static readonly int ECoreNotInitialized					= -60001;
        public static readonly int ECoreSDKObjectNull					= -60002;
        public static readonly int ECoreArgumentNull					= -60003;
        public static readonly int ECoreInitializeWinsockFailure		= -60004;
        public static readonly int ECoreUserNameAuthNameEmpty			= -60005;
        public static readonly int ECoreInitiazeStackFailure			= -60006;
        public static readonly int ECorePortOutOfRange					= -60007;
        public static readonly int ECoreAddTcpTransportFailure			= -60008;
        public static readonly int ECoreAddTlsTransportFailure			= -60009;
        public static readonly int ECoreAddUdpTransportFailure			= -60010;
        public static readonly int ECoreMiniAudioPortOutOfRange		    = -60011;
        public static readonly int ECoreMaxAudioPortOutOfRange			= -60012;
        public static readonly int ECoreMiniVideoPortOutOfRange		    = -60013;
        public static readonly int ECoreMaxVideoPortOutOfRange			= -60014;
        public static readonly int ECoreMiniAudioPortNotEvenNumber		= -60015;
        public static readonly int ECoreMaxAudioPortNotEvenNumber		= -60016;
        public static readonly int ECoreMiniVideoPortNotEvenNumber		= -60017;
        public static readonly int ECoreMaxVideoPortNotEvenNumber		= -60018;
        public static readonly int ECoreAudioVideoPortOverlapped		= -60019;
        public static readonly int ECoreAudioVideoPortRangeTooSmall 	= -60020;
        public static readonly int ECoreAlreadyRegistered				= -60021;
        public static readonly int ECoreSIPServerEmpty					= -60022;
        public static readonly int ECoreExpiresValueTooSmall			= -60023;
        public static readonly int ECoreCallIdNotFound					= -60024;
        public static readonly int ECoreNotRegistered					= -60025;
        public static readonly int ECoreCalleeEmpty				    	= -60026;
        public static readonly int ECoreInvalidUri						= -60027;
        public static readonly int ECoreAudioVideoCodecEmpty			= -60028;
        public static readonly int ECoreNoFreeDialogSession			    = -60029;
        public static readonly int ECoreCreateAudioChannelFailed		= -60030;
        public static readonly int ECoreSessionTimerValueTooSmall		= -60040;
        public static readonly int ECoreAudioHandleNull				    = -60041;
        public static readonly int ECoreVideoHandleNull				    = -60042;
        public static readonly int ECoreCallIsClosed					= -60043;
        public static readonly int ECoreCallAlreadyHold				    = -60044;
        public static readonly int ECoreCallNotEstablished				= -60045;
        public static readonly int ECoreCallNotHold				    	= -60050;
        public static readonly int ECoreSipMessaegEmpty				    = -60051;
        public static readonly int ECoreSipHeaderNotExist				= -60052;
        public static readonly int ECoreSipHeaderValueEmpty		    	= -60053;
        public static readonly int ECoreSipHeaderBadFormed				= -60054;
        public static readonly int ECoreBufferTooSmall					= -60055;
        public static readonly int ECoreSipHeaderValueListEmpty		    = -60056;
        public static readonly int ECoreSipHeaderParserEmpty			= -60057;
        public static readonly int ECoreSipHeaderValueListNull			= -60058;
        public static readonly int ECoreSipHeaderNameEmpty				= -60059;
        public static readonly int ECoreAudioSampleNotmultiple			= -60060;	//	The audio sample should be multiple of 10
        public static readonly int ECoreAudioSampleOutOfRange			= -60061;	//	The audio sample range is 10 - 60
        public static readonly int ECoreInviteSessionNotFound			= -60062;
        public static readonly int ECoreStackException					= -60063;
        public static readonly int ECoreMimeTypeUnknown			    	= -60064;
        public static readonly int ECoreDataSizeTooLarge				= -60065;
        public static readonly int ECoreSessionNumsOutOfRange			= -60066;
        public static readonly int ECoreNotSupportCallbackMode			= -60067;
        public static readonly int ECoreNotFoundSubscribeId		    	= -60068;
        public static readonly int ECoreCodecNotSupport				    = -60069;
        public static readonly int ECoreCodecParameterNotSupport        = -60070;
        public static readonly int ECorePayloadOutofRange				= -60071;	//  Dynamic Payload range is 96 - 127
        public static readonly int ECorePayloadHasExist				    = -60072;	//  Duplicate Payload values are not allowed.
        public static readonly int ECoreFixPayloadCantChange            = -60073;   //  It's fixed payload type does not allows changed.
        public static readonly int ECoreCodecTypeInvalid				= -60074;
        public static readonly int ECoreCodecWasExist					= -60075;
        public static readonly int ECorePayloadTypeInvalid				= -60076;
        public static readonly int ECoreArgumentTooLong				    = -60077;
        public static readonly int ECoreMiniRtpPortMustIsEvenNum		= -60078;
        public static readonly int ECoreCallInHold						= -60079;
        public static readonly int ECoreNotIncomingCall				    = -60080;
        public static readonly int ECoreCreateMediaEngineFailure		= -60081;
        public static readonly int ECoreAudioCodecEmptyButAudioEnabled  = -60082;
        public static readonly int ECoreVideoCodecEmptyButVideoEnabled  = -60083;
        public static readonly int ECoreNetworkInterfaceUnavailable     = -60084;
        public static readonly int ECoreWrongDTMFTone                   = -60085;
        public static readonly int ECoreWrongLicenseKey                 = -60086;           
        public static readonly int ECoreTrialVersionLicenseKey			= -60087;
        public static readonly int ECoreOutgoingAudioMuted				= -60088;
        public static readonly int ECoreOutgoingVideoMuted              = -60089;


// audio
        public static readonly int EAudioFileNameEmpty					= -70000;
        public static readonly int EAudioChannelNotFound				= -70001;
        public static readonly int EAudioStartRecordFailure		    	= -70002;
        public static readonly int EAudioRegisterRecodingFailure		= -70003;
        public static readonly int EAudioRegisterPlaybackFailure		= -70004;
        public static readonly int EAudioGetStatisticsFailure			= -70005;
        public static readonly int EAudioIsPlaying						= -70006;
        public static readonly int EAudioPlayObjectNotExist		    	= -70007;
        public static readonly int EAudioPlaySteamNotEnabled			= -70008;
        public static readonly int EAudioRegisterCallbackFailure		= -70009;
        public static readonly int EAudioCreateAudioConferenceFailure	= -70010;
        public static readonly int EAudioOpenPlayFileFailure			= -70011;
        public static readonly int EAudioPlayFileModeNotSupport		    = -70012;
        public static readonly int EAudioPlayFileFormatNotSupport		= -70013;
        public static readonly int EAudioPlaySteamAlreadyEnabled		= -70014;
        public static readonly int EAudioCreateRecordFileFailure		= -70015;
        public static readonly int EAudioCodecNotSupport				= -70016;
        public static readonly int EAudioPlayFileNotEnabled			    = -70017;
        public static readonly int EAudioPlayFileUnknowSeekOrigin       = -70018;
        public static readonly int EAudioCantSetDeviceIdDuringCall      = -70019;

// video
        public static readonly int EVideoFileNameEmpty					= -80000;
        public static readonly int EVideoGetDeviceNameFailure			= -80001;
        public static readonly int EVideoGetDeviceIdFailure			    = -80002;
        public static readonly int EVideoStartCaptureFailure			= -80003;
        public static readonly int EVideoChannelNotFound				= -80004;
        public static readonly int EVideoStartSendFailure				= -80005;
        public static readonly int EVideoGetStatisticsFailure			= -80006;
        public static readonly int EVideoStartPlayAviFailure			= -80007;
        public static readonly int EVideoSendAviFileFailure             = -80008;
        public static readonly int EVideoRecordUnknowCodec              = -80009;
        public static readonly int EVideoCantSetDeviceIdDuringCall		= -80010;
        public static readonly int EVideoUnsupportCaptureRotate		    = -80011;
        public static readonly int EVideoUnsupportCaptureResolution     = -80012;


// Device
        public static readonly int EDeviceGetDeviceNameFailure         = -90001;
    }
}
