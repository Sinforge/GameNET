import Preview from "./Article/Preview";
const mockData = [
    {
        name : "How to get strong in game",
        shortDesc : "Its guide how to get strong in game always"
    },
    {
        name : "Template1",
        shortDesc : "Template1"
    },
    {
        name : "Template2",
        shortDesc : "Template2"
    },
];
export default function Main() {
    const previews = mockData.map(preview => 
        <Preview name={preview.name} shortDesc={preview.shortDesc}></Preview>
    );
    console.log(previews);  
    return (
        <ul>
            {previews}
        </ul>
    );
}