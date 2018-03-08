using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eventaris.Domain;

namespace EventarisXDal
{
    public interface IParticipationService
    {
        Task<bool> RegisterParticipation(Participation participation);
    }
}
