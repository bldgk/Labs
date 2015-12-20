import numpy as np

def polin(x,b):
    res=0
    for i in range(len(b)):
        res+=b[i]*x[0,i]
    return res 

def load_parabolic(file,with_b0=True):
    file = open(file,"r")
    temp=file.readline()
    matr_x=[]
    matr_y=[]
    for line in file.readlines(): 
        line=[float(str) for str in line.split(' ') ]    
        if with_b0:
            tmp_x=[1.]
        else: tmp_x=[]
        tmp_x.append(line[0])
        tmp_x.append(line[1])
        tmp_x.append(line[2])
        tmp_x.append(line[0]*line[1])
        tmp_x.append(line[0]*line[2])
        tmp_x.append(line[1]*line[2])   
        tmp_x.append(line[0]*line[0])
        tmp_x.append(line[1]*line[1])
        tmp_x.append(line[2]*line[2])
        tmp_y=(line[3])           
        matr_x.append(tmp_x)   
        matr_y.append(tmp_y)
    matr_x=np.matrix(matr_x,"float")
    matr_y=np.matrix(matr_y,"float")
    matr_y=matr_y.reshape(20,1)
    file.close()
    return matr_x, matr_y 

def load_linear(file='01.txt',with_b0=True):
    file = open(file,"r")
    temp=file.readline()
    matr_x=[]
    matr_y=[]
    for line in file.readlines(): 
        line=[float(str) for str in line.split(' ') ]    
        if with_b0:
            tmp_x=[1.]
        else: tmp_x=[]
        tmp_x.append(line[0])
        tmp_x.append(line[1])
        tmp_x.append(line[2])        
        tmp_x.append(line[0]*line[1])
        tmp_x.append(line[0]*line[2])
        tmp_x.append(line[1]*line[2])   
        tmp_x.append(line[0]*line[1]*line[2])
        tmp_y=(line[3])      
        matr_x.append(tmp_x)   
        matr_y.append(tmp_y)
    matr_x=np.matrix(matr_x,"float")
    matr_y=np.matrix(matr_y,"float")
    matr_y=matr_y.reshape(20,1)
    file.close()
    return matr_x, matr_y 


def lssquares(x,y):
    b_matrix=np.array([])
    b_matrix=np.dot(x.T,x)
    b_matrix=np.dot(np.linalg.inv(b_matrix),x.T)
    b_matrix=np.dot(b_matrix,y)
    return b_matrix