﻿using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_KhoaHocOnl.ViewModel;

namespace QL_KhoaHocOnl.Controllers
{
    public class QuizzController : Controller
    {

        public QL_COURSEEntities db = new QL_COURSEEntities();
        public string selectQuizVM = "";
        // GET: Quizz
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SelectQuizz()
        {
            QuizVM quiz = new ViewModel.QuizVM();
            quiz.ListOfQuizz = db.Quizs.Select(q => new SelectListItem
            {
                Text = q.QuizName,
                Value = q.QuizID.ToString()

            }).ToList();

            return View(quiz);
        }

        [HttpPost]
        public ActionResult SelectQuizz(QuizVM quiz)
        {
            selectQuizVM = quiz.QuizName;
            QuizVM quizSelected = db.Quizs.Where(q => q.QuizID == quiz.QuizID).Select(q => new QuizVM
            {
                QuizID = q.QuizID,
                QuizName = q.QuizName,

            }).FirstOrDefault();

            if (quizSelected != null)
            {
                Session["SelectedQuiz"] = quizSelected;

                return RedirectToAction("QuizTest");
            }

            return View();
        }

        [HttpGet]
        public ActionResult QuizTest()
        {
            QuizVM quizSelected = Session["SelectedQuiz"] as QuizVM;
            IQueryable<QuestionVM> questions = null;

            if (quizSelected != null)
            {

                questions = db.Questions.Where(q => q.Quiz.QuizID == quizSelected.QuizID)
                   .Select(q => new QuestionVM
                   {
                       QuestionID = q.QuestionID,
                       QuestionText = q.QuestionText,
                       Choices = q.Choices.Select(c => new ChoiceVM
                       {
                           ChoiceID = c.ChoiceID,
                           ChoiceText = c.ChoiceText
                       }).ToList()

                   }).AsQueryable();


            }

            return View(questions);
        }

        [HttpPost]
        public ActionResult QuizTest(List<QuizAnswersVM> resultQuiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<ViewModel.QuizAnswersVM>();

            foreach (QuizAnswersVM answser in resultQuiz)
            {
                QuizAnswersVM result = db.Answers.Where(a => a.QuestionID == answser.QuestionID).Select(a => new QuizAnswersVM
                {
                    QuestionID = a.QuestionID.Value,
                    AnswerQ = a.AnswerText,
                    isCorrect = (answser.AnswerQ.ToLower().Equals(a.AnswerText.ToLower()))

                }).FirstOrDefault();

                finalResultQuiz.Add(result);
            }

            return Json(new { result = finalResultQuiz }, JsonRequestBehavior.AllowGet);
        }
    }
}