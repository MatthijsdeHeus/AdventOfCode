from Utilities import *

data = GetData(2022, 6)


def part1and2(input, frame):
    stream = list(input[0])

    currentList = []
    for x in range(len(stream)):
        currentList.append(stream[x])

        if x > frame - 1:
            currentList.pop(0)

        if x > frame - 2:
            if len(currentList) == len(set(currentList)):
                return x + 1


print(part1and2(data, 4))
print(part1and2(data, 14))
