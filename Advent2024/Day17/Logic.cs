using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day17
{
    public class Logic
    {
        private Model _model;
        public Logic(Model model)
        {
            _model = model;
        }

        private int GetComboOperand(int operand)
        {
            switch (operand)
            {
                case 4:
                    return _model.A;
                case 5:
                    return _model.B;
                case 6:
                    return _model.C;
                default:
                    return operand;
            }
        }

        private bool Adv(int operandX)
        {
            var operand = GetComboOperand(operandX);
            var div = (int)Math.Pow(2, operand);
            var value = _model.A / div;
            _model.A = value;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Bdv(int operandX)
        {
            var operand = GetComboOperand(operandX);
            var div = (int)Math.Pow(2, operand);
            var value = _model.A / div;
            _model.B = value;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Cdv(int operandX)
        {
            var operand = GetComboOperand(operandX);
            var div = (int)Math.Pow(2, operand);
            var value = _model.A / div;
            _model.C = value;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Bst(int operandX)
        {
            var operand = GetComboOperand(operandX);
            _model.B = operand % 8;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Out(int operandX)
        {
            var operand = GetComboOperand(operandX);
            var val = operand % 8;
            _model.Output.Add(val);
            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Jnz(int operand)
        {
            if (_model.A == 0)
            {
                _model.PP += 2;
                return _model.PP < _model.Program.Count;
            }

            _model.PP = operand;
            return _model.PP < _model.Program.Count;
        }

        private bool Bxc(int operandX)
        {
            var operand = GetComboOperand(operandX);

            var val = _model.B ^ _model.C;
            _model.B = val;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool Bxl(int operand)
        {
            var val = _model.B ^ operand;
            _model.B = val;

            _model.PP += 2;
            return _model.PP < _model.Program.Count;
        }

        private bool ExecuteCommand()
        {
            var opcode = _model.Program[_model.PP];
            var operand = _model.Program[_model.PP + 1];

            if (opcode == 0) return Adv(operand);
            if (opcode == 1) return Bxl(operand);
            if (opcode == 2) return Bst(operand);
            if (opcode == 3) return Jnz(operand);
            if (opcode == 4) return Bxc(operand);
            if (opcode == 5) return Out(operand);
            if (opcode == 6) return Bdv(operand);
            if (opcode == 7) return Cdv(operand);

            throw new NotImplementedException();
        }

        public string Run()
        {
            long sum = 0;

            while (ExecuteCommand())
            {
            }

            sum = _model.Output.Count;
            var s = string.Join(',', _model.Output);

            return sum.ToString();
        }
    }
}
