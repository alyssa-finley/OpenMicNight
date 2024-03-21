using OpenMicNight.Data;
using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Logic
{
    public interface ISignUpListLogic
    {
        void AddPerformanceToSignUpList(Performance performance);
        void AddPerformanceToSignUpListById(Performance performance);
        bool RemovePerformanceFromSignUpListById(Performance performance);
        SignUpList GetSignUpList();
        int GetIndexToInsertByPerformanceType(Performance performance);
    }
}
