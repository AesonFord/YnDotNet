using Xunit;

namespace YnDotNet.Tests
{
    public class YnTests
    {
        [Theory]
        [InlineData("y", true)]
        [InlineData("yes", true)]
        [InlineData("true", true)]
        [InlineData("1", true)]
        [InlineData("on", true)]
        [InlineData("Y", true)]
        [InlineData("YES", true)]
        [InlineData("TRUE", true)]
        [InlineData("ON", true)]
        [InlineData("enabled", true)]
        [InlineData("n", false)]
        [InlineData("no", false)]
        [InlineData("false", false)]
        [InlineData("0", false)]
        [InlineData("off", false)]
        [InlineData("N", false)]
        [InlineData("NO", false)]
        [InlineData("FALSE", false)]
        [InlineData("OFF", false)]
        [InlineData("disabled", false)]

        public void Parse_StandardValues_ReturnsExpectedResult(string input, bool expected)
        {
            // Act
            bool? result = Yn.Parse(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("maybe")]
        [InlineData("perhaps")]
        [InlineData("not sure")]
        [InlineData("")]
        [InlineData(" ")]
        public void Parse_NonStandardValues_ReturnsDefault(string input)
        {
            // Act
            bool? result = Yn.Parse(input);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("maybe", true)]
        [InlineData("perhaps", false)]
        [InlineData("not sure", true)]
        public void Parse_WithDefaultValue_ReturnsProvidedDefault(string input, bool defaultValue)
        {
            // Act
            bool? result = Yn.Parse(input, false, defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Theory]
        [InlineData(null)]
        public void Parse_WithNullInput_ReturnsDefault(object input)
        {
            // Act
            bool? result = Yn.Parse(input);
            bool? resultWithDefault = Yn.Parse(input, false, true);

            // Assert
            Assert.Null(result);
            Assert.True(resultWithDefault);
        }

        [Fact]
        public void Parse_WithNonStringObject_ParsesObjectToString()
        {
            // Arrange
            var trueObject = new CustomToString("yes");
            var falseObject = new CustomToString("no");
            var unknownObject = new CustomToString("maybe");

            // Act
            bool? trueResult = Yn.Parse(trueObject);
            bool? falseResult = Yn.Parse(falseObject);
            bool? unknownResult = Yn.Parse(unknownObject);

            // Assert
            Assert.True(trueResult);
            Assert.False(falseResult);
            Assert.Null(unknownResult);
        }

        // Lenient parsing tests
        [Theory]
        [InlineData("yws", true)]  // Close to "yes"
        [InlineData("yea", true)]
        [InlineData("yas", true)]
        [InlineData("nah", false)] // Close to "no"
        [InlineData("ni", false)]
        [InlineData("nope", false)]
        public void Parse_LenientMode_RecognizesSimilarValues(string input, bool expected)
        {
            // Act
            bool? result = Yn.Parse(input, true);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("xyz")]
        [InlineData("abc")]
        [InlineData("qwe")]
        public void Parse_LenientMode_StillReturnsDefaultForUnrelatedValues(string input)
        {
            // Arrange
            bool? defaultValue = true;

            // Act
            bool? result = Yn.Parse(input, true, defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Theory]
        [InlineData("  yes  ", true)] // Test trimming
        [InlineData("  no  ", false)] // Test trimming
        public void Parse_TrimsInput_BeforeProcessing(string input, bool expected)
        {
            // Act
            bool? result = Yn.Parse(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Helper class that overrides ToString() for testing non-string object parsing
        /// </summary>
        private class CustomToString
        {
            private readonly string _value;

            public CustomToString(string value)
            {
                _value = value;
            }

            public override string ToString()
            {
                return _value;
            }
        }
    }
}