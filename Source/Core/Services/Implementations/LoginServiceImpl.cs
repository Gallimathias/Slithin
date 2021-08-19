﻿using System.Linq;
using LiteDB;
using Slithin.Core.Scripting;

namespace Slithin.Core.Services.Implementations
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly LiteDatabase _db;
        private readonly EventStorage _events;

        public LoginServiceImpl(LiteDatabase db, EventStorage events)
        {
            _db = db;
            _events = events;
        }

        public LoginInfo GetLoginCredentials()
        {
            var collection = _db.GetCollection<LoginInfo>();

            if (collection.Count() == 1)
            {
                return collection.FindAll().First();
            }
            else
            {
                return new(null, null, false);
            }
        }

        public void RememberLoginCredencials(LoginInfo info)
        {
            var collection = _db.GetCollection<LoginInfo>();

            if (collection.Count() == 1)
            {
                var old = collection.FindAll().First();
                info._id = old._id;

                collection.Update(info);
            }
            else
            {
                collection.Insert(info);
            }
        }
    }
}
