tx = ty = linspace (-1, 1, 41)';
[xx, yy] = meshgrid (tx, ty);
r = sqrt (xx .^ 2 + yy .^ 2) + eps;
tz = (0.5*sin (pi*r) + 0.5);
mesh (tx, ty, tz);