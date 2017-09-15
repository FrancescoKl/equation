using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assignment
{
	public class EquationSolver
	{
		
		public EquationSolver(){
		}

        private static List<Member>[] parse_input(String input)
		{
            Regex regex = new Regex(@"[(\d)X]*");
			List<string> equation = new List<string>(input.Split(' '));
			List<Member> variable_side = new List<Member>();
			List<Member> literal_side = new List<Member>();

			if (equation[0] != "calc"){
				System.Console.WriteLine("no calc");
				System.Environment.Exit(1);
			}

            Boolean left_side = true;

            for (int i = 1; i < equation.Count; i++){
                if (equation[i] == '=') left_side = false;
                if (regex.Match(equation[i])){
                    string value = regex.Match(equation[i]).Groups[1].Value;
                    Boolean positive = equation[i - 1] == '-' ? false : true;
                    Boolean variable = equation[i].Contains('X') ? true : false;
                    if ((variable && !left_side) || (!variable && left_side)) positive = !positive;
                    Member member = new Member(value, positive);
                    if (variable) variable_side.Add(member);
                    else literal_side.Add(member);
                }
            }

            List<Member>[] splitted_equation = new List<Member>[2];
            splitted_equation[0] = variable_side;
            splitted_equation[1] = literal_side;

			return splitted_equation;
		}

		

        static void Main() {
            String equation = Console.ReadLine();
			List<Member>[] splitted_equation = parse_input(equation);
		}
	}

    public class Member{

        public int value;
        public Boolean positive;

        public Member(string value, Boolean positive){
            this.value = (int)value;
            this.positive = positive;
        }

    }

}

