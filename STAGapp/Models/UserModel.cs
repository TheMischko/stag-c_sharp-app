using STAGapp.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAGapp.Models
{
    public enum Roles
    {
        Student, Teacher, Admin, Prorector, Institute, Secretary
    }

    public static class UserModel
    {
        private static StagLoginTicket _user;
        private static string _authToken;

        private static Dictionary<string, Roles> _rolesCodes;

        public static void SetUser(StagLoginTicket user)
        {
            _user = user;
            _rolesCodes = new Dictionary<string, Roles>()
            {
                {"AD", Roles.Admin },
                {"VY", Roles.Teacher },
                {"ST", Roles.Student },
                {"PR", Roles.Prorector },
                {"KA", Roles.Institute },
                {"FA", Roles.Secretary }
            };
        }

        public static StagLoginTicket GetUser()
        {
            if(_user.Equals(default(StagLoginTicket)))
            {
                throw new NoUserLoggedInException();
            } else
            {
                return (StagLoginTicket)_user;
            }
        }

        public static string GetAuthToken()
        {
            if(_user.Equals(default(StagLoginTicket)))
            {
                throw new NoUserLoggedInException();
            } else
            {
                return _user.Token;
            }
        }

        public static bool IsUserInRole(Roles role)
        {
            string roleCode = GetRoleCode(role);
            

            if(String.IsNullOrEmpty(roleCode)) return false;

            foreach (StagUserInfo userInfo in _user.StagUserInfo)
            {
                if (userInfo.Role == roleCode) return true;
            }

            return false;
        }

        public static string GetIDByRole(Roles role)
        {
            string roleCode = GetRoleCode(role);
            
            foreach (StagUserInfo userInfo in _user.StagUserInfo)
            {
                if (userInfo.Role == roleCode)
                {
                    StagUserInfo user = userInfo;
                    if (String.IsNullOrEmpty(user.UcitIdno))
                    {
                        return user.OsCislo;
                    }
                    else
                    {
                        return user.UcitIdno;
                    }
                }
            }

            throw new UserIsNotInRole();
            
        }

        private static string GetRoleCode(Roles role)
        {
            string roleCode = "";
            foreach (KeyValuePair<string, Roles> roleDictItem in _rolesCodes)
            {
                if (roleDictItem.Value == role)
                {
                    roleCode = roleDictItem.Key;
                    break;
                }
            }
            return roleCode;
        }

    }

    public class NoUserLoggedInException : Exception{}
    public class UserIsNotInRole : Exception { }
}
