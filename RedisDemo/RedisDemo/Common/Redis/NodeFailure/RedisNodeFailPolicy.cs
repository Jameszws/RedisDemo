//using System.Collections.Generic;

//using ServiceStack.Redis.Configuration;
//using ServiceStack.Redis.Locator;
//using ServiceStack.Redis.FailurePolicy;


//namespace RedisDemo.Common.Redis.NodeFailure
//{
//    /// <summary>
//    /// 节点宕机策略
//    /// </summary>
//    public sealed class RedisNodeFailPolicy : INodeFailurePolicy
//    {
//        private RedisNode _node;

//        public RedisNodeFailPolicy(RedisNode node)
//        {
//            this._node = node;
//        }

//        bool INodeFailurePolicy.ShouldFail()
//        {
//            //LogManager.WriteException("【严重】Redis节点宕机，请速检查：" + this._Node.EndPoint.Address.ToString() + ":" + this._Node.EndPoint.Port);
//            return true;
//        }
//    }

//    /// <summary>
//    /// 节点宕机工厂类
//    /// </summary>
//    public class RedisNodeFailPolicyFactory : INodeFailurePolicyFactory, IProviderFactory<INodeFailurePolicyFactory>
//    {
//        INodeFailurePolicy INodeFailurePolicyFactory.Create(RedisNode node)
//        {
//            return new RedisNodeFailPolicy(node);
//        }

//        INodeFailurePolicyFactory IProviderFactory<INodeFailurePolicyFactory>.Create()
//        {
//            return new RedisNodeFailPolicyFactory();
//        }

//        void IProvider.Initialize(Dictionary<string, string> parameters)
//        { }
//    }
//}
