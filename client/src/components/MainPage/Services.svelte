<script>
    import Service from "./Service.svelte";

</script>
<div class="hero bg-base-100">
    <div class="hero-content text-center">
      <div class="max-w-md">
        <h1 class="text-5xl font-bold">Services</h1>
        <p class="py-6">Most Important Services That We Provide</p>
       
      </div>
    </div>
</div>
<div class="flex flex-col w-full lg:flex-row">

    <Service title="Revit/Autocad API Integration" 
    desc="We can integrate with your favorite software, AutoCAD or Revit, using the .NET API,
     Also create Windows GUI software that can seamlessly work with them."
     imgPaths={["/icons/revit.png","/icons/autocad.png","/icons/api.png","/icons/dotnet.png"]} />

    <div class="divider lg:divider-horizontal"></div> 

    <Service title="Web Design/Development" 
    desc="Creating captivating user experiences using the latest frontend technologies. Our focus is on responsive design,
     interactive interfaces, and fast loading times to provide users with a seamless and engaging web experience." 
    imgPaths={["/icons/svelte.png","/icons/react.png","/icons/tailwind.png","/icons/ts.png",]} />
    <div class="divider lg:divider-horizontal"></div> 

    <Service title="Building Data Intensive Backend Infrastructure" 
    desc="Engineering robust backend infrastructure for handling extensive data operations. Our solutions ensure scalability,
     data processing efficiency, and reliability for managing data-intensive tasks." 
    imgPaths={["/icons/aspnet.png","/icons/docker.png","/icons/postgre.png","/icons/redis.png","/icons/nginx.png"]} />

  </div>