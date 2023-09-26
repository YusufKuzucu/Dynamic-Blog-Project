using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategory Adını Boş Geçemezsiniz ");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kategory Açıklamasını Boş Geçemezsiniz ");
            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategory Adını En Fazla 50 Karakter Olmalıdır");
            RuleFor(x => x.CategoryName).MinimumLength(2).WithMessage("Kategory Adını En Az 50 Karakter Olmalıdır");
        }
    }
}
