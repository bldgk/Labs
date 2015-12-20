import numpy as np
import random
import math
from fcns import *
from matplotlib import pyplot as plt

file='02.txt'
loads=[load_parabolic(file),load_linear(file)]
plots=[]

for load in loads:
    matr_x,matr_y=load
    matr_x=np.matrix(matr_x,"float")
    matr_y=np.matrix(matr_y,"float")
    paralelexp=[]
    for i in range(20):
        if i+1==8 or i+1==9 or i+1==16 or i+1==20:
            paralelexp.append(matr_y[i,0])

    b_matrix=lssquares(matr_x,matr_y)
    print u"Коэффициенты, найденные МНК:"
    print b_matrix
    print  
   
    SumRelativelyToAverage=0
    MinimalSumOfSquareDeviatiations = 0
    SumParalelExps = 0
    for i in range(20):
        MinimalSumOfSquareDeviatiations+=np.sum(np.square(matr_y[i]-polin(matr_x[i],b_matrix)))    
        SumRelativelyToAverage+=np.sum(np.square(matr_y[i]-np.average(matr_y)))
    for i in range(len(paralelexp)):
        SumParalelExps+=np.sum(np.square(paralelexp[i]-np.average(paralelexp)))
     
    f1 = 20.-len(b_matrix)
    f2 = len(paralelexp)-1
    
    print u"Подсчитаем степени свободы:"
    print u"f1 = n - p, n - колличество экспериментов, p - колличество оцениваемых b"
    print u"f2 = m - 1, m - колличество паралельных экспериментов"
    print u"f1=%d f2=%d"%(f1,f2)
    print    

    print u"Остаточная дисперсия Sz^2 = S/f1, S - минимальная сума квадратов отклонений."
    Sz = MinimalSumOfSquareDeviatiations/f1
    print Sz
    print u"Дисперсия воспроизводимости Sr^2 = Spar/f2"
    Sr = SumParalelExps/f2
    print Sr
    print u"Дисперсия относительно среднего Sy^2 = S<y>/n-1"
    Sy = SumRelativelyToAverage/19.
    print Sy
 
    print u"Критерий Фишера"
    print Sz/Sr
   
    res=[]    
    for l in matr_x:
        res.append(np.sum(polin(l,b_matrix)))   
    plots.append(res)


i=221
for p in plots:
    plt.subplot(i)
    plt.plot(matr_y[:,0],'r',range(20),p,'g')
    i+=1
plt.show()
    #print np.linalg.lstsq(matr_x,matr_y)