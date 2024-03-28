using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Logic
{
    public interface ISignUpLogic : IPerformerLogic, ISongLogic
    {
        void AddPerformanceToSignUpList(Performer performance);
        void AddPerformanceToSignUpListById(int id);
        bool RemovePerformanceFromSignUpListById(int id);
        bool MaxPerformances();
        SignUpList GetSignUpList();
        int GetIndexToInsertByPerformanceType(Performer performance);
    }
}
