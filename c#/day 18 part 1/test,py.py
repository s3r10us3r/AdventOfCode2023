def polygonArea(X, Y, n):
 
    # Initialize area
    area = 0.0
 
    # Calculate value of shoelace formula
    j = n - 1
    for i in range(0,n):
        area += (X[j] + X[i]) * (Y[j] - Y[i])
        j = i   # j is previous vertex to i
     
 
    # Return absolute value
    return int(abs(area / 2.0))
 
# Driver program to test above function
X = [0,6,6,4,6,6,1,1,0,0,2,2,0]
Y = [0,0,-5,-7,-7,-9,-9,-7,-7,-5,-5,-2,-2]
n = len(X)
print(polygonArea(X, Y, n))