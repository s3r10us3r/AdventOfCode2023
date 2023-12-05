import re


def getNextAmount(cv, seed):
    for c in cv:
        if c[1] <= seed < c[1] + c[2]:
            return c[0] - c[1] + seed
    return seed


def getNextRanges(cv, rng):
    ranges, i = [], 0
    while True:
        c = cv[i]
        if c[1] > rng[0]:
            if c[1] > rng[1]:
                ranges.append(rng)
                return ranges
            ranges.append((rng[0], c[1] - 1))
            rng = c[1], rng[1]
        elif c[1] <= rng[0] < c[1] + c[2]:
            offset = c[0] - c[1]
            if rng[1] < c[1] + c[2]:
                ranges.append(tuple(r + offset for r in rng))
                return ranges
            ranges.append((rng[0] + offset, c[0] + c[2]))
            rng = c[1] + c[2], rng[1]
        elif c[1] + c[2] <= rng[0]:
            i += 1
            if i == len(cv):
                break
    ranges.append(rng)
    return ranges


def main():
    with open("input", "r") as f:
        res = f.read().strip().split("\n\n")
        seeds = list(map(int, re.findall("\d+", res[0])))
        ranges = [(seeds[i], seeds[i] + seeds[i + 1]) for i in range(0, len(seeds), 2)]
        for cs in res[1:]:
            c = list(map(int, re.findall("\d+", cs)))
            # sort for part 2
            cv = sorted([c[k : k + 3] for k in range(0, len(c), 3)], key=lambda x: x[1])
            seeds = [getNextAmount(cv, s) for s in seeds]
            ranges = sum((getNextRanges(cv, rng) for rng in ranges), [])
        print(min(seeds), next(map(min, zip(*ranges))))


if __name__ == "__main__":
    main()