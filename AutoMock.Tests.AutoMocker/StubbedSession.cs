namespace AutoMock.Tests.AutoMocker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Moq;

    public class StubbedSession : ISession
    {
        public StubbedSession(Mock<IHttpContextAccessor> mockContentAccessor) : this()
        {
           
            mockContentAccessor.SetupProperty(x => x.HttpContext,
                                              new Mock<HttpContext>()
                                                  .SetupProperty(x => x.Session, this)
                                                  .Object);
            
        }

        public StubbedSession()
        {
            this.GetDictionary = new Dictionary<string, byte[]>();
            this.Keys = new List<string>();
        }

        public Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            this.GetDictionary.TryGetValue(key, out value);
            return true;
        }

        public void Set(string key, byte[] value)
        {
            this.GetDictionary.Add(key, value);
            Keys = Keys.Concat(new List<string> { key });
        }

        public void Remove(string key)
        {
            throw new NotImplementedException(nameof(Remove));
        }

        public void Clear()
        {
            GetDictionary.Clear();
            Keys = new List<string>();
        }

        public Dictionary<string, byte[]> GetDictionary { get; set; }
        public string Id { get; } = "000";
        public bool IsAvailable { get; }
        public IEnumerable<string> Keys { get; set; }
    }
}
