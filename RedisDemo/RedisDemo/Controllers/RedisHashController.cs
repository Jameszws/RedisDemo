using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using RedisDemo.Common;
using RedisDemo.Common.ActionResultExtend;
using RedisDemo.Common.Redis;
using RedisDemo.Common.Redis.RedisOperator;
using RedisDemo.Models;
using ServiceStack.Common.Utils;
using ServiceStack.Redis;

namespace RedisDemo.Controllers
{

    public class RedisHashController : Controller
    {
        public ActionResult Index()
        {
            var hashOperator = new HashOperator();
            var data = hashOperator.GetAllEntriesFromHash(ConstValue.UserHashId);
            Hashtable aa=new Hashtable();
            return View();
        }

        public ActionResult GetAll()
        {
            var hashOperator = new HashOperator();
            var data = hashOperator.GetAll<User>(ConstValue.UserHashId);
            return new JsonCamelResult(new
            {
                Data = data
            });
        }


        public ActionResult Get(string id)
        {
            var hashOperator = new HashOperator();
            try
            {
                var key = "key-" + id;
                var user = hashOperator.Get<User>(ConstValue.UserHashId, key);
                return new JsonCamelResult(new
                {
                    Code = "1",
                    Data = user
                });
            }
            catch (Exception)
            {
                return new JsonCamelResult(new
                {
                    Code = "0",
                    Msg = "fail!",
                });
            }

        }

        public ActionResult Delete(string id)
        {
            var hashOperator = new HashOperator();
            try
            {
                var key = "key-" + id;
                var boolRet = hashOperator.Remove(ConstValue.UserHashId, key);
                return new JsonCamelResult(new
                {
                    Code = boolRet ? "1" : "0",
                });
            }
            catch (Exception)
            {
                return new JsonCamelResult(new
                {
                    Code = "0",
                    Msg = "fail!"
                });
            }

        }

        public ActionResult Add(string userName, string position)
        {
            var suff = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var hashOperator = new HashOperator();
            var userToStore = new User()
            {
                Id = suff,
                UserName = userName,
                Job = new Job { Position = position }
            };
            try
            {
                var key = "key-" + suff;
                hashOperator.Set(ConstValue.UserHashId, key, userToStore);
            }
            catch (Exception)
            {
                return new JsonCamelResult(new
                {
                    Code = "0",
                    Msg = "fail!"
                });
            }
            return new JsonCamelResult(new
            {
                Code = "1"
            });
        }

        public ActionResult Update(string id, string userName, string position)
        {

            try
            {
                var hashOperator = new HashOperator();
                var user = hashOperator.GetAll<User>(ConstValue.UserHashId).Find(x => x.Id == id);
                if (user == null)
                {
                    return new JsonCamelResult(new
                    {
                        Code = "0",
                        Msg = "该员工不存在！"
                    });
                }
                var userToStore = new User()
                {
                    Id = user.Id,
                    UserName = userName,
                    Job = new Job { Position = position }
                };
                hashOperator.Set(ConstValue.UserHashId, "key-" + user.Id, userToStore);
            }
            catch (Exception)
            {
                return new JsonCamelResult(new
                {
                    Code = "0",
                    Msg = "fail!"
                });

            }
            return new JsonCamelResult(new
            {
                Code = "1"
            });
        }
    }
}
