using FluentAssertions;
using MagicCarRepairAISupported.Application.Common.TextEncoding;
using System.Text;
using Xunit;

namespace MagicCarRepairAISupported.Application.Tests.Common.TextEncoding
{
    public class Utf8MojibakeHelperTests
    {
        [Fact]
        public void Repair_RoundTrip_Koruğu()
        {
            var correct = "Körüğü";
            var utf8 = Encoding.UTF8.GetBytes(correct);
            var corrupted = Encoding.Latin1.GetString(utf8);

            Utf8MojibakeHelper.Repair(corrupted).Should().Be(correct);
        }

        [Fact]
        public void Repair_UserExample_RotDis()
        {
            var correct = "Rot Diş Köreği";
            var utf8 = Encoding.UTF8.GetBytes(correct);
            var corrupted = Encoding.Latin1.GetString(utf8);

            Utf8MojibakeHelper.Repair(corrupted).Should().Be(correct);
        }

        [Fact]
        public void Repair_LeavesCleanTurkishUnchanged()
        {
            var s = "Fren Balata Seti (Ön)";
            Utf8MojibakeHelper.Repair(s).Should().Be(s);
        }

        [Fact]
        public void LooksLikeUtf8Mojibake_DetectsCorruption()
        {
            var correct = "Körüğü";
            var utf8 = Encoding.UTF8.GetBytes(correct);
            var corrupted = Encoding.Latin1.GetString(utf8);

            Utf8MojibakeHelper.LooksLikeUtf8Mojibake(corrupted).Should().BeTrue();
            Utf8MojibakeHelper.LooksLikeUtf8Mojibake(correct).Should().BeFalse();
        }
    }
}
