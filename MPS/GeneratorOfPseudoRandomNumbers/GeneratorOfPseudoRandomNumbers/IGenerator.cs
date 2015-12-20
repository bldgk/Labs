using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorOfPseudoRandomNumbers
{
    public interface IGenerator 
    {
		double CriterionX(List<int> frequencies);
		List<int> FrequenciesTest();
		double Method(double x);
    }
}
