using NUnit.Framework;
using Library.Helpers;
using Moq;
using Library.Contracts.PublicAPI.Helpers;

namespace Library.InputReaders
{
    [TestFixture]
    public class NumberInputReaderTests
    {
        private Mock<IInputReader<int>> _inputReader;

        [SetUp]
        public void Setup()
        {
            _inputReader = new Mock<IInputReader<int>>();
        }

        [Test]
        public void NumberInputReader_ReceivesValidNumber_ReturnsTheNumber()
        {
            _inputReader.Setup(i=>i.)
            var reader = new NumberInputReader();
            reader.Read();
        }
    }
}
