using MVVMTest.ClsPublic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMTest.Entitys
{
   public abstract class Entity
    {
        private Guid _id;
        public bool IsTransient()
        {
            return this._id == Guid.Empty;
        }
        /// <summary>
        /// Generate identity for this entity
        /// </summary>
        public Guid GenerateNewIdentity()
        {
            if (IsTransient())
                this._id = IdentityGenerator.NewSequentialGuid();

            return this._id;
        }
    }
}
