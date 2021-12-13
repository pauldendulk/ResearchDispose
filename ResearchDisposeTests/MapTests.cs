using NUnit.Framework;
using ResearchDispose;
using System;

namespace ResearchDisposeTests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void TestMapDispose()
        {
            var weak = CreateMap();

            TryDispose(weak);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(weak.IsAlive);
        }

        [Test]
        [Ignore("This test fails. Perhaps it accesses the Target field within this method itself")]
        public void TestMapDisposeWithoutTryDisposeMethod()
        {
            var weak = CreateMap();

            if (weak.Target is IDisposable disposable)
                disposable.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(weak.IsAlive);
        }

        private static void TryDispose(WeakReference weak)
        {
            if (weak.Target is IDisposable disposable)
                disposable.Dispose();
        }

        private static WeakReference CreateMap()
        {
            var map = new Map();
            var weak = new WeakReference(map);
            return weak;
        }
    }
}
