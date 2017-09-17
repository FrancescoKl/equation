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
            Regex regex = new Regex(@"\b(\d+X?|X)\b");
			List<string> equation = new List<string>(input.Split(' '));
			List<Member> variable_side = new List<Member>();
			List<Member> literal_side = new List<Member>();

			if (equation[0] != "calc"){
				System.Console.WriteLine("no calc");
				System.Environment.Exit(1);
			}

            Boolean left_side = true;

            for (int i = 1; i < equation.Count; i++){
                if (equation[i] == "=") left_side = false;
                if (regex.IsMatch(equation[i])){
                    
                    System.Console.WriteLine(equation[i]);

                    string value = regex.Match(equation[i]).Groups[1].Value;
                    if (value.Contains("X")){
                        System.Console.WriteLine("variable");
                        value = value.Replace("X", "");
                        if (value == "") value = "1";
                    }
                    System.Console.WriteLine("value");
                    System.Console.WriteLine(value);

                    Boolean positive = equation[i - 1] == "-" ? false : true;
                    Boolean variable = equation[i].Contains("X") ? true : false;
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

        private static string resolve_equation(List<Member>[] equation){

            int x = 0;
            int literal = 0;

            foreach (Member value in equation[0]){
                if (value.positive) x += value.value;
                else x -= value.value;
            }

            foreach (Member value in equation[1]){
                System.Console.WriteLine(literal);
                if (value.positive) literal += value.value;
				else literal -= value.value;
				System.Console.WriteLine("after");
				System.Console.WriteLine(literal);
            }

            String result = String.Format("Result: X = {0}", (literal / x).ToString());
            return result;
        }

        public static void Main() {
            String equation = Console.ReadLine();
			List<Member>[] splitted_equation = parse_input(equation);
            String result = resolve_equation(splitted_equation);
            System.Console.Write(result);
		}
	}

    public class Member{

        public int value;
        public Boolean positive;

        public Member(string value, Boolean positive){
            this.value = Int32.Parse(value);
            this.positive = positive;
        }

    }

}

