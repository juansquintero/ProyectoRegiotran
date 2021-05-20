using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Regiotran.ViewModels;
using 

namespace UITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {            
        }
       
        
        [Test]
        public void OnBoardingTest()
        {
            var onBoardingAnimationViewModel = new OnBoardingAnimationViewModel();
            using (new AssertionScope())
            {
                onBoardingAnimationViewModel.NextButtonText.Should().Be("Siguiente");
            }
        }
    }
}