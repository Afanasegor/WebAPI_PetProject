using CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public class Validator
    {
        private static readonly Regex nameRegex = new Regex(@"^[a-zA-z]+$");
        private static readonly Regex numberRegex = new Regex(@"^(\+)?[0-9]{11}$");
        private static readonly Regex emailRegex = new Regex(@"^([a-z0-9_\.-]+)@([a-z0-9_\.-]+)\.([a-z\.]{2,6})$");
        public static void ValidateStudent(Student student)
        {

            if (!nameRegex.IsMatch(student.Name))
            {
                throw new InvalidStudentException($"Students Name ({student.Name}) is not in correct format...", "student.Name");
            }

            if (!nameRegex.IsMatch(student.Surname))
            {
                throw new InvalidStudentException($"Students Surname ({student.Surname}) is not in correct format...", "student.Surname");
            }

            if (!numberRegex.IsMatch(student.PhoneNumber))
            {
                throw new InvalidStudentException($"Phone number ({student.PhoneNumber}) is not in correct format... (Correct ex.: 89274690937 or +79274690937)");
            }

            if (!emailRegex.IsMatch(student.Email))
            {
                throw new InvalidStudentException($"Email ({student.Email}) is not in correct format...", "student.Email");
            }
        }

        public static void ValidateLecturer(Lecturer lecturer)
        {
            if (!nameRegex.IsMatch(lecturer.Name))
            {
                throw new InvalidLecturerException($"Lecturers Name ({lecturer.Name}) is not in correct format...", "lecturer.Name");
            }

            if (!nameRegex.IsMatch(lecturer.Surname))
            {
                throw new InvalidLecturerException($"Lecturers Surname ({lecturer.Surname}) is not in correct format...", "lecturer.Surname");
            }

            if (!emailRegex.IsMatch(lecturer.Email))
            {
                throw new InvalidStudentException($"Email ({lecturer.Email}) is not in correct format...", "lecturer.Email");
            }
        }

        public static bool NameIsValid(string input)
        {
            return nameRegex.IsMatch(input);
        }

        public static bool PhoneNumberIsValid(string input)
        {
            return numberRegex.IsMatch(input);
        }
        public static bool EmailIsValid(string input) 
        {
            return emailRegex.IsMatch(input);
        }

        public static bool HomeworkMarkIsValid(int homeworkMark)
        {
            if (homeworkMark >= 0 && homeworkMark < 6)
            {
                return true;
            }
            return false;
        }
    }
}
