using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoFakeItEasy;

namespace Lab05.Domain.Tests
{
    public partial class AutofixtureTests 
    {
        public class AutoFakeItEasyAttribute : AutoDataAttribute
    {
        public AutoFakeItEasyAttribute() : base(() => new Fixture().Customize(new AutoFakeItEasyCustomization()))
        {

        }
    }
}