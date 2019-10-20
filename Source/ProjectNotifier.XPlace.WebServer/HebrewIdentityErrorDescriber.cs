namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// A Hebrew translated IdentityErrorDescriber
    /// </summary>
    public class HebrewIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = $".ארתה בעיה לא ידוע"
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError()
            {
                Code = nameof(ConcurrencyFailure),
                Description = ".כישלון במקביל אופטימי, האובייקט שונה",
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = ".סיסמאות לא תואמות"
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError()
            {
                Code = nameof(InvalidToken),
                Description = ".טוקן לא תקין"
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError()
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = ".משתמש עם הפרטים האלו כבר קיים"
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = $".לא תקין '{userName}' שם המשתמש"
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = $".לא תקין '{email}' האימייל"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = $".כבר תפוס '{userName}' שם המשתמש"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = $".כבר תפוס '{email}' האימייל"
            };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidRoleName),
                Description = $".לא תקין'{role}' התפקיד"
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = $".כבר קיים '{role}' התפקיד"
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError()
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = ".למשתמש כבר יש סיסמא"
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError()
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = ".משתמש לא  מופעל",
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = $".'{role}' המשתמש כבר שייך לתפקיד"
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserNotInRole),
                Description = $".'{role}' המשתמש לא שייך לתפקיד"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = $".({length}) סיסמא קצרה מדי"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = ".סיסמא לא תקינה, נדרש אותיות",
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = ".סיסמא לא תקינה, נדרש ספרות",
            };
        }


        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = ".סיסמא לא תקינה, נדרש אותיות קטנות",
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = ".סיסמא לא תקינה, נדרש אותיות גדולות",
            };
        }

    };
};