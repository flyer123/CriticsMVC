using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Test_Google_Api.Models.SaveModels;

namespace Test_Google_Api.Models.ViewModel
{
    public class ReviewModel
    {
        //название ресторана
        public string Name { get; set; }

        //оценка за обслуживание
        public int? ServiceMark { get; set; }

        //оценка за интерьер
        public int? InteriorMark { get; set; }

        //оценка за кухню
        public int? KitchenMark { get; set; }

        //текст рецензии
        public string Text { get; set; }

        public string Label { get; set; }

        //адресс ресторана
        public string Address { get; set; }

        //дата создания рецензии по этому адресу
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        //координаты широта
        public double? Lat { get; set; }

        //координаты долгота
        public double? Lng { get; set; }

        //id адреса ресторана
        public int Id { get; set; }

        //тип кухни
        public string KitchenType { get; set; }

        //время работы
        public string WorkHours { get; set; }

        //средний счет
        public int? Amount { get; set; }

        //для детей
        public string Children { get; set; }

        //предложения ресторана
        public string Propositions { get;set;}

        //музыка
        public string Music { get; set; }

        //телефоны ресторана
        public string Phone { get; set; }
        public int? NetwrokId { get; set; }

        public string CustomLabel { get; set; }

    }
}