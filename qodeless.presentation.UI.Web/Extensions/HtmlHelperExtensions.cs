using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qodeless.presentation.UI.Web.Models;
using qodeless.presentation.WebApp.Models;

namespace qodeless.presentation.UI.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static UbuntuBuilder Start(this IHtmlHelper htmlHelper)
        { 
           return new();
        }
    }

    //Fluent Class UI
    public class UbuntuBuilder
    {
        private StringBuilder buffer { get; set; }
        private Controller Controller { get; set; }
        private string Token { get; set; }
        public UbuntuBuilder()
        {
            buffer = new StringBuilder();
        }

        public IHtmlContent Build(ViewModelBase vm)
        {
            AddDefaultButtons();
            AddFormEnd(vm.Id);
            return new HtmlString(buffer.ToString());
        }
        
        public UbuntuBuilder InputText(string id, object value, int col)
        {

            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<input id='{id}' name='{id}' type='text' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder InputNumber(string id, object value, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label  class='control-label'>{id}</label>");
            buffer.Append($"<input id='{id}' name='{id}' type='number' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder DropDown(string id, object value, List<SelectListItem> options, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<select id='{id}' name='{id}' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            foreach (var option in options)
            {
                buffer.Append($"<option '{ (option.Selected ? "selected=selected" : "") }' value='{option.Value}'>{option.Text}</option>");
            }
            buffer.Append("</select>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder DateTime(string id, object value, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<input type='date' id='{id}' name='{id}' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder CheckBox(string id, bool value, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<input type='checkbox' id='{id}' name='{id}' class='form-control form-control-sm' value='{ (value == true ? "checked" : "") }' data-val='true'/>");
            buffer.Append("</select>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder TextArea(string id, object value, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<select id='{id}' name='{id}' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            buffer.Append("</select>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder Image(string id, object value, int col = 3)
        {
            buffer.Append($"<div class='col-{col}'>");
            buffer.Append("<div class='form-group'>");
            buffer.Append($"<label class='control-label'>{id}</label>");
            buffer.Append($"<input id='{id}' name='{id}' class='form-control form-control-sm' value='{ (value != null ? value.ToString() : string.Empty) }' data-val='true'/>");
            buffer.Append("</select>");
            buffer.Append("</div>");
            buffer.Append($"<span class='text-danger field-validation-valid' data-valmsg-for='{id}' data-valmsg-replace='true'></span>");
            buffer.Append("</div>");
            return this;
        }

        public UbuntuBuilder AddRowStart()
        {
            buffer.Append("<div class='row'>");
            return this;
        }

        public UbuntuBuilder AddRowEnd()
        {
            buffer.Append("</div>");
            return this;
        }

        private void AddDefaultButtons()
        {
            buffer.Append("<br/><div class='col-12 mt-12'><input type='button' value='Voltar' class='btn btn-secondary btn-sm w-150' onclick='history.back(-1)' />" +
                          " | <input type = 'submit' value='Cadastrar' class='btn btn-success btn-sm w-150' /></div>");
        }

        private void AddFormEnd(Guid id)
        {
            buffer.Append("<input name='Id' type = 'hidden' value='" + id + "'>");
            //buffer.Append("<input name='__RequestVerificationToken' type = 'hidden' value='"+ Token + "'>");
            buffer.Append("</div></div></div></div></form>");
        }

        public UbuntuBuilder SetInfo(string title,string subtitle)
        {
            buffer.Append($"<h4>{title}</h4>");
            SetSubTitle(subtitle);
            return this;
        }

        public UbuntuBuilder SetController(Controller controller)
        {
            Controller = controller;
            return this;
        }

        private void SetSubTitle(string subtitle)
        {
            buffer.Append("<form action='" + Controller.ControllerContext.ActionDescriptor.ActionName + "' method='post'>" +
                "<div class='row'>" +
                "<div asp-validation-summary='ModelOnly' class='text-danger'></div>" +
                "<div class='col-12'>" +
                "<div class='card mt-2'>" +
                "<div class='card-body'>" +
                "<div class='card-title'>" +
                "<h6>"+ subtitle +"</h6> " +
                "</div>");
        }

        public UbuntuBuilder AddNewRow()
        {
            buffer.Append("</div><div class='row'>");
            return this;
        }
    }
}
