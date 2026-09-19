shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D screen_texture : source_color, hint_screen_texture, filter_nearest;
uniform float dither_repeat = 1.0;
uniform float dither_strength = 1.0;
uniform float color_levels = 4.0;

const float bayer[16] = float[](
	0.0 / 16.0, 8.0 / 16.0, 2.0 / 16.0, 10.0 / 16.0,
	12.0 / 16.0, 4.0 / 16.0, 14.0 / 16.0, 6.0 / 16.0,
	3.0 / 16.0, 11.0 / 16.0, 1.0 / 16.0, 9.0 / 16.0,
	15.0 / 16.0, 7.0 / 16.0, 13.0 / 16.0, 5.0 / 16.0
);

void vertex() {
	POSITION = vec4(VERTEX.xy, 1.0, 1.0);
}

void fragment() {
	vec3 color_in = texture(screen_texture, SCREEN_UV).rgb;
	vec2 pixel_coord = SCREEN_UV * VIEWPORT_SIZE / dither_repeat;
	int x = int(mod(pixel_coord.x, 4.0));
	int y = int(mod(pixel_coord.y, 4.0));
	vec3 dithered = floor(color_in * color_levels + bayer[y * 4 + x]) / color_levels;
	ALBEDO = mix(color_in, dithered, dither_strength);
}
