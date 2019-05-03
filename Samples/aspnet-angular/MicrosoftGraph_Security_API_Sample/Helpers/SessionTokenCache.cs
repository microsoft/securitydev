// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System.Threading;

namespace MicrosoftGraph_Security_API_Sample.Helpers
{
    public class SessionTokenCache
    {
        private static ReaderWriterLockSlim sessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private readonly string _userId = string.Empty;
        private readonly string _cacheId = string.Empty;
        private readonly HttpContext _httpContext = null;

        private TokenCache _cache = new TokenCache();

        public SessionTokenCache(string userId, HttpContext httpcontext)
        {
            // not object, we want the SUB
            _userId = userId;
            _cacheId = _userId + "_TokenCache";
            _httpContext = httpcontext;
            Load();
        }

        public TokenCache GetMsalCacheInstance()
        {
            _cache.SetBeforeAccess(BeforeAccessNotification);
            _cache.SetAfterAccess(AfterAccessNotification);
            Load();
            return _cache;
        }

        public void SaveUserStateValue(string state)
        {
            sessionLock.EnterWriteLock();
            sessionLock.ExitWriteLock();
        }

        public string ReadUserStateValue()
        {
            string state = string.Empty;
            sessionLock.EnterReadLock();
            sessionLock.ExitReadLock();
            return state;
        }

        public void Load()
        {
            sessionLock.EnterReadLock();
            sessionLock.ExitReadLock();
        }

        public void Persist()
        {
            sessionLock.EnterWriteLock();

            // Optimistically set HasStateChanged to false. We need to do it early to avoid losing changes made by a concurrent thread.
#pragma warning disable CS0618 // Type or member is obsolete
            _cache.HasStateChanged = false;
#pragma warning restore CS0618 // Type or member is obsolete

            // Reflect changes in the persistent store
            //this.httpContext.Session[this.cacheId] = this.cache.Serialize();
            sessionLock.ExitWriteLock();
        }

        // Triggered right before MSAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        private void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            this.Load();
        }

        // Triggered right after MSAL accessed the cache.
        private void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
#pragma warning disable CS0618 // Type or member is obsolete
            if (_cache.HasStateChanged)
#pragma warning restore CS0618 // Type or member is obsolete
            {
                this.Persist();
            }
        }
    }
}