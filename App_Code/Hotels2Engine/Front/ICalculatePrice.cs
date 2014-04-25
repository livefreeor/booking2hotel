using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ICalculatePrice
/// </summary>
public interface ICalculatePrice
{
    int ConditionID { get; set; }
    DateTime DateCheck { get; set; }
    void Calculate();
}