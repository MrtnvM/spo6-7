using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L6_7
{
    class LexicAnalyzer
    {
        public List<string> Keywords = new List<string>(); 
        public List<string> Identificators = new List<string>();
        public List<string> Constants = new List<string>(); 
        public List<string> Operations = new List<string>(); 
        public List<string> Info = new List<string>(); 

        public LexicAnalyzer(string code)
        {
            code = code.Trim();
            if (!code.StartsWith("for"))
            {
                Info.Add("Error: Выражение должно начинаться с ключевого слова for");
                return;
            }
            Info.Add("Ключествое слово for. Начало цикла");
            Keywords.Add("for");

            code = code.Substring(3).TrimStart();

            if (!code.StartsWith("("))
            {
                Info.Add("Error: после ключего слова for должены идти 3 блока в формате (инициализация; условие; шаг)");
                return;
            }
            Info.Add("Начало блока инициализации");

            var paranthesIndex = code.IndexOf(")", StringComparison.Ordinal);

            if (paranthesIndex < 0)
            {
                Info.Add("Error: отсутствует закрывающая скобка цикла for");
                return;
            }

            var forComponents = code.Substring(1, paranthesIndex - 1).Split(';');
            if (forComponents.Length != 3)
            {
                Info.Add("Error: в цикле for должно содержаться только 3 блока");
                return;
            }

            var initBlock = forComponents[0].Trim();
            Info.Add("Блок инициализации: " + initBlock);

            if (string.IsNullOrEmpty(initBlock))
            {
                Info.Add("Блок инициализации пуст");
            }
            else
            {
                var componentsOfInitBlock = initBlock.Split(new[] { ":=" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .ToArray();

                if (componentsOfInitBlock.Length != 2)
                {
                    Info.Add("Error: блок инициализации должен содержать либо корректную операцию присваивания либо быть пустым");
                    return;
                }
                Operations.Add("Операция присваивания: :=");

                if (!IsIdentifier(componentsOfInitBlock.First()))
                {
                    Info.Add("Error: в операции присваивания первым операндом должен быть идентификатор");
                    return;
                }

                if (!IsNumber(componentsOfInitBlock.Last()))
                {
                    if (!IsIdentifier(componentsOfInitBlock.Last()))
                    {
                        Info.Add("Error: некорректная операция присваивания в блоке инициализации");
                        return;
                    }
                }
            }

            var conditionBlock = forComponents[1].Trim();
            Info.Add("Блок с условием: " + conditionBlock);

            if (string.IsNullOrEmpty(conditionBlock))
            {
                Info.Add("Warning: блок с условием пуст. Цикл является бесконечным");
            }
            else
            {
                var componentsOfConditionBlock = conditionBlock.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                if (componentsOfConditionBlock.Length != 3)
                {
                    Info.Add("Error: Некорректное выражение в блоке с условием. Условие должно быть в таком формате: <идентификатор или константа> <операция сравнения> <идентификатор или константа>");
                    return;
                }

                if (!IsComparisonOperator(componentsOfConditionBlock[1]))
                {
                    Info.Add("Error: некорректный оператор сравнения");
                    return;
                }

                if (!IsNumber(componentsOfConditionBlock[0]) && !IsIdentifier(componentsOfConditionBlock[0]))
                {
                    Info.Add("Error: некорректный идентификатор или константа в блоке условия цикла for");
                    return;
                }

                if (!IsNumber(componentsOfConditionBlock[2]) && !IsIdentifier(componentsOfConditionBlock[2]))
                {
                    Info.Add("Error: некорректный идентификатор или константа в блоке условия цикла for");
                    return;
                }
            }

            var stepBlock = forComponents[2].Trim();
            Info.Add("Блок с шагом цикла: " + stepBlock);

            if (string.IsNullOrEmpty(stepBlock))
            {
                Info.Add("Блок с шагом цикла пуст");
            }
            else
            {
                if (stepBlock.EndsWith("++"))
                {
                    var step = stepBlock.Substring(0, stepBlock.Length - 2);
                    if (IsIdentifier(step))
                    {
                        Operations.Add("Операция постфиксного инкремента: <идентификатор>++");
                    }
                    else
                    {
                        Info.Add("Error: некорректная операция инкремента в блоке шага цикла for");
                        return;
                    }
                }
                else if (stepBlock.StartsWith("++"))
                {
                    var step = stepBlock.Substring(2);
                    if (IsIdentifier(step))
                    {
                        Operations.Add("Операция префиксного инкремента: ++<идентификатор>");
                    }
                    else
                    {
                        Info.Add("Error: некорректная операция инкремента в блоке шага цикла for");
                        return;
                    }
                }
                else if (stepBlock.EndsWith("--"))
                {
                    var step = stepBlock.Substring(0, stepBlock.Length - 2);
                    if (IsIdentifier(step))
                    {
                        Operations.Add("Операция постфиксного декремента: <идентификатор>--");
                    }
                    else
                    {
                        Info.Add("Error: некорректная операция декремента в блоке шага цикла for");
                        return;
                    }
                }
                else if (stepBlock.StartsWith("--"))
                {
                    var step = stepBlock.Substring(2);
                    if (IsIdentifier(step))
                    {
                        Operations.Add("Операция префиксного декремента: --<идентификатор>");
                    }
                    else
                    {
                        Info.Add("Error: некорректная операция декремента в блоке шага цикла for");
                        return;
                    }
                }
                else
                {
                    var changingOperations = new[] {"+=", "-=", "*=", "/=", "%="};
                    if (changingOperations.Count(o => stepBlock.Contains(o)) == 1)
                    {
                        foreach (var changingOperation in changingOperations)
                        {
                            if (stepBlock.Contains(changingOperation))
                            {
                                Operations.Add("Операция присваивания: " + changingOperation);
                            }
                        }

                        var componentsOfStepBlock = stepBlock.Split(changingOperations, StringSplitOptions.RemoveEmptyEntries);
                        if (componentsOfStepBlock.Length != 2)
                        {
                            Info.Add("Error: некорректная операция в блоке шага цикла for");
                            return;
                        }

                        if (!IsIdentifier(componentsOfStepBlock[0]))
                        {
                            Info.Add("Error: некорректная операция в блоке шага цикла for");
                            return;
                        }

                        if (!IsIdentifier(componentsOfStepBlock[1]) && !IsNumber(componentsOfStepBlock[1]))
                        {
                            Info.Add("Error: некорректная операция в блоке шага цикла for");
                            return;
                        }
                    }
                    else
                    {
                        Info.Add("Error: некорректная операция в блоке шага цикла for");
                        return;
                    }
                }
            }

            if (code.Length == paranthesIndex + 1)
            {
                Info.Add("Error: блок for не завершен");
                return;
            }
            var afterForBlocks = code.Substring(paranthesIndex + 1).Trim();

            if (afterForBlocks == "do")
            {
                Keywords.Add(afterForBlocks);
            }
            else
            {
                Info.Add("Error: заголовок цикла должен заканчиваться ключевым словом do");
                return;
            }
        }

        private bool IsIdentifier(string id)
        {
            id = id.Trim();
            if (string.IsNullOrEmpty(id))
            {
                Info.Add("Error: некорректный идентификатор. Идентификатор не может быть пустой строкой");
                return false;
            }

            if (id.Any(c => !char.IsLetterOrDigit(c)))
            {
                Info.Add("Error: идентификатор должен состоять только из букв латинского алфавита или цифр");
                return false;
            }

            if (char.IsDigit(id[0]))
            {
                Info.Add("Error: идентификатор не может начинаться с цифры");
                return false;
            }

            Identificators.Add(id);

            return true;
        }

        private bool IsNumber(string value)
        {
            value = value.Trim();

            if (value.All(char.IsDigit))
            {
                Constants.Add(value);
                return true;
            }

            bool dotsMoreThan1 = value.Count(c => c == '.') > 1;
            bool countOfDotsDoesnotEqualCountOfNotDigits = value.Count(c => !char.IsDigit(c)) !=  value.Count(c => c == '.');

            if (dotsMoreThan1 || countOfDotsDoesnotEqualCountOfNotDigits)
                return false;

            if (value.Count(c => c == '.') > 1)
                return false;

            if (value.StartsWith(".") || value.EndsWith("."))
                return false;

            Constants.Add(value);

            return true;
        }

        private bool IsComparisonOperator(string value)
        {
            value = value.Trim();
            var comparisonOperators = new[] {">", "<", "=", ">=", "<="};

            var isComparisonOperator = comparisonOperators.Contains(value);

            if (isComparisonOperator)
                Operations.Add("Оператор сравнения: " + value);

            return isComparisonOperator;
        }
    }
}
