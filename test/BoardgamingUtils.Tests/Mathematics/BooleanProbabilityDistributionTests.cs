// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
// This file is part of Boardgaming Utils.
//
// Boardgaming Utils is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utils is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Boardgaming Utils. If not, see <http://www.gnu.org/licenses/>.

using FluentAssertions;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Tests.Mathematics;

public class BooleanProbabilityDistributionTests
{
    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(0.7, 0.3)]
    [InlineData(1.0, 0.0)]
    public void GivenSuccessProbabilityWhenConstructingThenDistributionHasExpectedState(
        double probabilityValue,
        double expectedFailureProbabilityValue)
    {
        var successProbability = new Probability(probabilityValue);
        var distribution = new BooleanProbabilityDistribution(successProbability);

        distribution.SuccessProbability.Should().Be(successProbability);
        distribution.FailureProbability.Equals(new Probability(expectedFailureProbabilityValue), 1e-6).Should().BeTrue(
            "Because actual failure probability of {0} is expected to be same as {1}", distribution.FailureProbability, expectedFailureProbabilityValue
        );
    }
}