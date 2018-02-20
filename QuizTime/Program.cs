using System;
using System.Collections.Generic;

namespace QuizTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Quiz quiz1 = new Quiz("LC101");
            List<string> q2answer = new List<string> { "a", "b" };
            MultiChoice q1 = new MultiChoice("For what could you use a for loop? \nA. Print to screen \nB. Assign a " +
                "variable \nC. Go through a series of list items.", "c");
            Checkbox q2 = new Checkbox("What languages have we worked with so far: \nA. Python \nB. HTML " +
                "\nC. C++", q2answer);
            TrueFalse q3 = new TrueFalse("Java is the same as JavaScript", "f");
            quiz1.AddQuestion(q1);
            quiz1.AddQuestion(q2);
            quiz1.AddQuestion(q3);
            quiz1.TakeQuiz();



        }
    }

    class Quiz
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        public void TakeQuiz()
        {
            double numCorrect = 0;
            foreach (Question question in Questions)
            {
                question.PrintQuestion();
                if (question.CheckAnswer()) { numCorrect += 1; }
            }
            string percent = ((numCorrect / Questions.Count) * 100).ToString();
         
            Console.WriteLine("You scored " + numCorrect.ToString() + " out of " + 
                Questions.Count.ToString() + ": " + percent + "%");
            Console.ReadLine();
        }
    }


    public abstract class Question
    {
        private int Id { get; set; }
        public string Query { get; internal set; }
        private static int newId = 0;

        public Question(int id, string query)
        {
            Id = id;
            Query = query;
        }

        public Question(string query) : this(newId, query)
        { newId += 1; }

        public abstract bool CheckAnswer();

        public void PrintQuestion()
        {
            Console.WriteLine(this.Query);
        }
    }

    public class MultiChoice : Question
    {
        public string Answer { get; set; }

        public MultiChoice(string query, string answer) : base(query)
        {
            Query = query;
            Answer = answer;
        }

        public override bool CheckAnswer()
        {
            Console.WriteLine("Enter a letter and press return.");
            string response = Console.ReadLine();
            if (response == Answer) { return true; }
            else { return false; }
        }
    }

    public class Checkbox : Question
    {
        public List<string> Answer { get; set; }

        public Checkbox(string query, List<string> answer) : base(query)
        {
            Query = query;
            Answer = answer;
        }

        public override bool CheckAnswer()
        {
            string response = "";
            List<string> responses = new List<string>();
            Console.WriteLine("Enter a letter and press return. Enter 'x' when finished");
            while (response != "x")
            {
                response = Console.ReadLine();
                responses.Add(response);              
            }

            if (responses == Answer) { return true; }
            else { return false; }
        }
    }

    public class TrueFalse : Question
    {
        public string Answer { get; set; }

        public TrueFalse(string query, string answer) : base(query)
        {
            Query = query;
            Answer = answer;
        }

        public override bool CheckAnswer()
        {
            Console.WriteLine("Enter a 't' for True or 'f' for False and press return.");
            string response = Console.ReadLine();
            if (response == Answer) { return true; }
            else { return false; }
        }
    }

}
