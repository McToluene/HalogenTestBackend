using System.Collections.Generic;
using HalogenTest.Dtos;

namespace HalogenTest.Interfaces
{
    public interface INumberResolver
    {
        List<Result> Process();
    }
}