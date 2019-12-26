using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions
{
    public interface IPipeTransform
    {
        string Name { get; }
        object Transform(object obj, params string[] args);
    }
}
