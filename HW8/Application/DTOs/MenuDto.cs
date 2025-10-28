namespace HW8.Application.DTOs
{
    public class MenuDto
    {
        public class MenuItemDto
        {
            public string Name { get; set; } = string.Empty;
            public List<MenuItemDto> SubItems { get; set; } = new();
        }

        public class MenuResponse
        {
            public List<MenuItemDto> Items { get; set; } = new();
        }
    }
}
