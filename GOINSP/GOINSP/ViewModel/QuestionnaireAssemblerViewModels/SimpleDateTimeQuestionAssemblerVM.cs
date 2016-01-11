﻿using GalaSoft.MvvmLight;
using GOINSP.ViewModel.QuestionnaireViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GOINSP.ViewModel.QuestionnaireAssemblerViewModels
{
    public class SimpleDateTimeQuestionAssemblerVM : ViewModelBase, IAssemblerVM
    {
        SimpleDateTimeQuestionVM attachedQuestion;

        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }

        public string AssemblerName { get; set; }

        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
                RaisePropertyChanged("Question");
            }
        }

        public SimpleDateTimeQuestionAssemblerVM()
        {
            AssemblerName = "Datum en Tijd Vraag";
            Clean();
        }

        public void Attach(SimpleDateTimeQuestionVM question)
        {
            attachedQuestion = question;
            Question = attachedQuestion.Question;
        }

        public void Update()
        {
            if(attachedQuestion != null)
            {
                attachedQuestion.Question = Question;
            }
            Clean();
        }

        public SimpleDateTimeQuestionVM Create()
        {
            SimpleDateTimeQuestionVM tempIntegerTextQuestion = new SimpleDateTimeQuestionVM() { Question = Question, Answer = DateTime.Now, Visible = Visibility.Visible };
            Clean();

            return tempIntegerTextQuestion;
        }

        public void OnFocus()
        {
            Clean();
            Visibility = Visibility.Visible;
        }

        public void Clean()
        {
            Visibility = Visibility.Collapsed;
            Question = "";
            attachedQuestion = null;
        }
    }
}
