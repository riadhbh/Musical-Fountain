#include "VuMeter.h"

void VuMeter_Init(){
GPIO_InitTypeDef  GPIO_InitStructure; 
                                  /*VM1*/  
  /* Enable the GPIO_LED Clock */  
  RCC_AHB1PeriphClockCmd(VM1_GPIO_CLK, ENABLE);  
  /* Configure the GPIO_LED pin */  
  GPIO_InitStructure.GPIO_Pin   = VM1_DATA_PIN ; 
  GPIO_InitStructure.GPIO_Mode  = GPIO_Mode_OUT;   
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP; 
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;  
  GPIO_InitStructure.GPIO_PuPd  = GPIO_PuPd_NOPULL;  
  GPIO_Init(VM1_GPIO_PORT, &GPIO_InitStructure);  
  
                                  /*VM2*/  
  /* Enable the GPIO_LED Clock */  
  RCC_AHB1PeriphClockCmd(VM2_GPIO_CLK, ENABLE);  
  /* Configure the GPIO_LED pin */  
  GPIO_InitStructure.GPIO_Pin   = VM2_DATA_PIN; 
  GPIO_InitStructure.GPIO_Mode  = GPIO_Mode_OUT;   
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP; 
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;  
  GPIO_InitStructure.GPIO_PuPd  = GPIO_PuPd_NOPULL;  
  GPIO_Init(VM2_GPIO_PORT, &GPIO_InitStructure);

                                  /*VM3*/  
  /* Enable the GPIO_LED Clock */  
  RCC_AHB1PeriphClockCmd(VM3_GPIO_CLK, ENABLE);  
  /* Configure the GPIO_LED pin */  
  GPIO_InitStructure.GPIO_Pin   = VM3_DATA_PIN; 
  GPIO_InitStructure.GPIO_Mode  = GPIO_Mode_OUT;   
  GPIO_InitStructure.GPIO_OType = GPIO_OType_PP; 
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;  
  GPIO_InitStructure.GPIO_PuPd  = GPIO_PuPd_NOPULL;  
  GPIO_Init(VM3_GPIO_PORT, &GPIO_InitStructure);     
}

void VuMeter_Send_Data(uint16_t data,uint8_t line){
if(data <=255){
if(line==1){
//VM1

VM1_GPIO_PORT->BSRRL=data<<1;//sending 1 bits of data 
VM1_GPIO_PORT->BSRRH=0;//sending 0 bits of data

}else if(line==2){
//VM2

VM2_GPIO_PORT->BSRRL=data<<7;//sending 1 bits of data 
VM2_GPIO_PORT->BSRRH=0;//sending 0 bits of data

}else{
//VM2

VM3_GPIO_PORT->BSRRL=data<<8;//sending 1 bits of data 
VM3_GPIO_PORT->BSRRH=0;//sending 0 bits of data

}
}else
return;
}

uint8_t pow(uint8_t a, uint8_t b){
uint8_t res=1;
if(a==0)
res = 0;
else
if(b==0)
res = 1;
else
for (int i = 1; i<= b ;i++){
res = res * a;
}
return res;
}

void VuMeter_Display(uint16_t level,uint8_t frequency){
  uint8_t data;
  if(level <= 8){
  data = pow(2,level) - 1;
  VuMeter_Send_Data(data,frequency); 
  }
else
return;
}