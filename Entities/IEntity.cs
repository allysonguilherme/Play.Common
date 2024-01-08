using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Play.Common.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}