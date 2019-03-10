//
// ProviderFactory.cs
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
using System.Reflection;
using System.Collections.Generic;

namespace Mono.Zeroconf.Providers
{
    internal static class ProviderFactory
    {
        private static IZeroconfProvider [] providers;
        private static IZeroconfProvider selected_provider;
    
        private static IZeroconfProvider DefaultProvider {
            get {
                if(providers == null) {
                    GetProviders();
                }
                
                return providers[0];
            }
        }
        
        public static IZeroconfProvider SelectedProvider {
            get { return selected_provider == null ? DefaultProvider : selected_provider; }
            set { selected_provider = value; }
        }
    
        /// <summary>
        /// This used to be a complicated mechanism that dynamically loaded
        /// platform-specific zeroconf providers. I only care about
        /// Windows and macOS, so bonjour is all I need.
        /// If bonjour isn't working on Windows, install iTunes.
        /// </summary>
        /// <returns>The providers.</returns>
        private static IZeroconfProvider[] GetProviders()
        {
            if (ProviderFactory.providers != null)
                return ProviderFactory.providers;

            var providersList = new List<IZeroconfProvider>();

            var provider = new Bonjour.ZeroconfProvider();
            provider.Initialize();

            providersList.Add(provider);

            if (providersList.Count == 0)
                throw new Exception("No Zeroconf providers could be found or initialized. Necessary daemon may not be running.");

            ProviderFactory.providers = providersList.ToArray();

            return ProviderFactory.providers;
        }
    }
}
