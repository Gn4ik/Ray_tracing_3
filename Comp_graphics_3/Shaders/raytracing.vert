#version 450
in vec3 vPosition; 
out vec3 glPosition;
void main (void)
{
 	gl_Position = vec4(vPosition, 1.0);
 	glPosition = vPosition;
}