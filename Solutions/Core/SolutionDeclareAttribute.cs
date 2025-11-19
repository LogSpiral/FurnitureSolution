using System;

namespace FurnitureSolution.Solutions.Core;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionDeclareAttribute(string solutionName, int rowIndex, int ingredientType, int dustType) : Attribute
{
    public string SolutionName { get; } = solutionName;
    public int RowIndex { get; } = rowIndex;
    public int IngredientType { get; } = ingredientType;
    public int DustType { get; } = dustType;
}
