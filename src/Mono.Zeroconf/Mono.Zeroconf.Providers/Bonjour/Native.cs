//
// Native.cs
//
// Authors:
//    Aaron Bockover  <abockover@novell.com>
//
// Copyright (C) 2006-2007 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Zeroconf.Providers.Bonjour
{
    public static class Native
    {
        // ServiceRef

        public static void DNSServiceRefDeallocate(IntPtr sdRef)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    NativeWindows.DNSServiceRefDeallocate(sdRef);
                    return;
                case OperatingSystem.OSX:
                    NativeOSX.DNSServiceRefDeallocate(sdRef);
                    return;
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ServiceError DNSServiceProcessResult(IntPtr sdRef)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceProcessResult(sdRef);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceProcessResult(sdRef);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static int DNSServiceRefSockFD(IntPtr sdRef)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceRefSockFD(sdRef);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceRefSockFD(sdRef);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ServiceError DNSServiceCreateConnection(out ServiceRef sdRef)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceCreateConnection(out sdRef);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceCreateConnection(out sdRef);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // DNSServiceBrowse

        public delegate void DNSServiceBrowseReply(ServiceRef sdRef,
                                                    ServiceFlags flags,
                                                    uint interfaceIndex,
                                                    ServiceError errorCode,
                                                    string serviceName,
                                                    string regtype,
                                                    string replyDomain,
                                                    IntPtr context);

        public static ServiceError DNSServiceBrowse(out ServiceRef sdRef,
                                                    ServiceFlags flags,
                                                    uint interfaceIndex,
                                                    string regtype,
                                                    string domain,
                                                    DNSServiceBrowseReply callBack,
                                                    IntPtr context)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceBrowse(out sdRef, flags, interfaceIndex, regtype, domain, callBack, context);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceBrowse(out sdRef, flags, interfaceIndex, regtype, domain, callBack, context);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // DNSServiceResolve

        public delegate void DNSServiceResolveReply(ServiceRef sdRef,
                                                     ServiceFlags flags,
                                                     uint interfaceIndex,
                                                     ServiceError errorCode,
                                                     string fullname,
                                                     string hosttarget,
                                                     ushort port,
                                                     ushort txtLen,
                                                     IntPtr txtRecord,
                                                     IntPtr context);

        public static ServiceError DNSServiceResolve(out ServiceRef sdRef,
                                                     ServiceFlags flags,
                                                     uint interfaceIndex,
                                                     string name,
                                                     string regtype,
                                                     string domain,
                                                     DNSServiceResolveReply callBack,
                                                     IntPtr context)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceResolve(out sdRef, flags, interfaceIndex, name, regtype, domain, callBack, context);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceResolve(out sdRef, flags, interfaceIndex, name, regtype, domain, callBack, context);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // DNSServiceRegister

        public delegate void DNSServiceRegisterReply(ServiceRef sdRef,
                                                      ServiceFlags flags,
                                                      ServiceError errorCode,
                                                      string name,
                                                      string regtype,
                                                      string domain,
                                                      IntPtr context);

        public static ServiceError DNSServiceRegister(out ServiceRef sdRef,
                                                      ServiceFlags flags,
                                                      uint interfaceIndex,
                                                      string name,
                                                      string regtype,
                                                      string domain,
                                                      string host,
                                                      ushort port,
                                                      ushort txtLen,
                                                      byte[] txtRecord,
                                                      DNSServiceRegisterReply callBack,
                                                      IntPtr context)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceRegister(out sdRef, flags, interfaceIndex, name, regtype, domain, host, port, txtLen, txtRecord, callBack, context);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceRegister(out sdRef, flags, interfaceIndex, name, regtype, domain, host, port, txtLen, txtRecord, callBack, context);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // DNSServiceQueryRecord

        public delegate void DNSServiceQueryRecordReply(ServiceRef sdRef,
                                                         ServiceFlags flags,
                                                         uint interfaceIndex,
                                                         ServiceError errorCode,
                                                         string fullname,
                                                         ServiceType rrtype,
                                                         ServiceClass rrclass,
                                                         ushort rdlen,
                                                         IntPtr rdata,
                                                         uint ttl,
                                                         IntPtr context);

        public static ServiceError DNSServiceQueryRecord(out ServiceRef sdRef,
                                                         ServiceFlags flags,
                                                         uint interfaceIndex,
                                                         string fullname,
                                                         ServiceType rrtype,
                                                         ServiceClass rrclass,
                                                         DNSServiceQueryRecordReply callBack,
                                                         IntPtr context)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.DNSServiceQueryRecord(out sdRef, flags, interfaceIndex, fullname, rrtype, rrclass, callBack, context);
                case OperatingSystem.OSX:
                    return NativeOSX.DNSServiceQueryRecord(out sdRef, flags, interfaceIndex, fullname, rrtype, rrclass, callBack, context);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // TXT Record Handling

        public static void TXTRecordCreate(IntPtr txtRecord, ushort bufferLen, IntPtr buffer)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    NativeWindows.TXTRecordCreate(txtRecord, bufferLen, buffer);
                    return;
                case OperatingSystem.OSX:
                    NativeOSX.TXTRecordCreate(txtRecord, bufferLen, buffer);
                    return;
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static void TXTRecordDeallocate(IntPtr txtRecord)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    NativeWindows.TXTRecordDeallocate(txtRecord);
                    return;
                case OperatingSystem.OSX:
                    NativeOSX.TXTRecordDeallocate(txtRecord);
                    return;
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ServiceError TXTRecordGetItemAtIndex(ushort txtLen,
                                                           IntPtr txtRecord,
                                                           ushort index,
                                                           ushort keyBufLen,
                                                           byte[] key,
                                                           out byte valueLen,
                                                           out IntPtr value)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordGetItemAtIndex(txtLen, txtRecord, index, keyBufLen, key, out valueLen, out value);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordGetItemAtIndex(txtLen, txtRecord, index, keyBufLen, key, out valueLen, out value);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ServiceError TXTRecordSetValue(IntPtr txtRecord,
                                                     byte[] key,
                                                     sbyte valueSize,
                                                     byte[] value)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordSetValue(txtRecord, key, valueSize, value);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordSetValue(txtRecord, key, valueSize, value);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ServiceError TXTRecordRemoveValue(IntPtr txtRecord, byte[] key)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordRemoveValue(txtRecord, key);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordRemoveValue(txtRecord, key);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ushort TXTRecordGetLength(IntPtr txtRecord)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordGetLength(txtRecord);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordGetLength(txtRecord);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static IntPtr TXTRecordGetBytesPtr(IntPtr txtRecord)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordGetBytesPtr(txtRecord);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordGetBytesPtr(txtRecord);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        public static ushort TXTRecordGetCount(ushort txtLen, IntPtr txtRecord)
        {
            switch (platform)
            {
                case OperatingSystem.Windows:
                    return NativeWindows.TXTRecordGetCount(txtLen, txtRecord);
                case OperatingSystem.OSX:
                    return NativeOSX.TXTRecordGetCount(txtLen, txtRecord);
                default:
                    throw new InvalidOperationException("The current OS is unsupported");
            }
        }

        // Platform Determination

        static OperatingSystem platform = OperatingSystem.Unknown;

        static Native()
        {
            platform = Native.GetCurrentOperatingSystem();
        }

        // Taken from https://stackoverflow.com/a/38795621
        private enum OperatingSystem
        {
            Unknown,
            Windows,
            OSX,
            Linux
        }

        private static OperatingSystem GetCurrentOperatingSystem()
        {
            var windir = Environment.GetEnvironmentVariable("windir");
            if (!string.IsNullOrEmpty(windir) && windir.Contains(@"\") && Directory.Exists(windir))
            {
                return OperatingSystem.Windows;
            }

            if (File.Exists(@"/proc/sys/kernel/ostype"))
            {
                string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
                if (osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
                {
                    // Note: Android gets here too
                    return OperatingSystem.Linux;
                }

                return OperatingSystem.Unknown;
            }

            if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                // Note: iOS gets here too
                return OperatingSystem.OSX;
            }

            return OperatingSystem.Unknown;
        }
    }
}
