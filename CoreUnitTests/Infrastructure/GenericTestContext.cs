using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class GenericTestContext<TSut>
{
    public GenericTestContext() => Fixture = new Fixture().Customize(new AutoMoqCustomization());

    protected Mock<T> MockOf<T>()
        where T : class =>
        Fixture.Freeze<Mock<T>>();

    protected IFixture Fixture { get; set; }

    protected TSut Sut => Fixture.Create<TSut>();
}
