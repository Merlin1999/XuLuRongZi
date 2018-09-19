using System;

namespace KT.UpdateServer.DB.Entities.Base
{
    public abstract class Entity
    {
        private string _id;

        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
