﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220503165331_AddMessageEntity")]
    partial class AddMessageEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Models.Entities.Comments.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Core.Models.Entities.Follows.Follow", b =>
                {
                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CereateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("FollowerId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Core.Models.Entities.Messagees.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReaded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRecieved")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("RecieverDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<string>("RecieverUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("SenderUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SnderDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("TheRecieverId")
                        .HasColumnType("int");

                    b.Property<int?>("TheSenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheRecieverId");

                    b.HasIndex("TheSenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Core.Models.Entities.Posts.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AngersCount")
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeleteDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("int");

                    b.Property<int>("FavesCount")
                        .HasColumnType("int");

                    b.Property<Guid>("GuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("LaughsCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int>("SadsCount")
                        .HasColumnType("int");

                    b.Property<int>("ViewsIpCount")
                        .HasColumnType("int");

                    b.Property<int>("ViewsTotalCount")
                        .HasColumnType("int");

                    b.Property<int>("ViewsUsersCount")
                        .HasColumnType("int");

                    b.Property<int>("WowsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Core.Models.Entities.Posts.PostLiked", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("PostId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("PostsLiked");
                });

            modelBuilder.Entity("Core.Models.Entities.User.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CoverPhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhotoUrlPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<Guid>("GuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Info")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ProfilePhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePhotoUrlPublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferalCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Models.Entities.User.UserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("PublicId")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Url")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserPhotos");
                });

            modelBuilder.Entity("Core.Models.Entities.Comments.Comment", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheAppUser")
                        .WithMany("TheCommentsList")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Entities.Posts.Post", "ThePost")
                        .WithMany("TheCommentsList")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TheAppUser");

                    b.Navigation("ThePost");
                });

            modelBuilder.Entity("Core.Models.Entities.Follows.Follow", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheFollowing")
                        .WithMany("TheFollowerList")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Models.Entities.User.AppUser", "TheFollower")
                        .WithMany("TheFollowingList")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TheFollower");

                    b.Navigation("TheFollowing");
                });

            modelBuilder.Entity("Core.Models.Entities.Messagees.Message", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheReciever")
                        .WithMany("TheRecievedMessagesList")
                        .HasForeignKey("TheRecieverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Models.Entities.User.AppUser", "TheSender")
                        .WithMany("TheSentMessagesList")
                        .HasForeignKey("TheSenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("TheReciever");

                    b.Navigation("TheSender");
                });

            modelBuilder.Entity("Core.Models.Entities.Posts.Post", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheAppUser")
                        .WithMany("ThePostsList")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TheAppUser");
                });

            modelBuilder.Entity("Core.Models.Entities.Posts.PostLiked", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheAppUser")
                        .WithMany("ThePostLikedList")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Entities.Posts.Post", "ThePost")
                        .WithMany("ThePostLikedList")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheAppUser");

                    b.Navigation("ThePost");
                });

            modelBuilder.Entity("Core.Models.Entities.User.UserPhoto", b =>
                {
                    b.HasOne("Core.Models.Entities.User.AppUser", "TheAppUser")
                        .WithMany("TheUserPhotosList")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TheAppUser");
                });

            modelBuilder.Entity("Core.Models.Entities.Posts.Post", b =>
                {
                    b.Navigation("TheCommentsList");

                    b.Navigation("ThePostLikedList");
                });

            modelBuilder.Entity("Core.Models.Entities.User.AppUser", b =>
                {
                    b.Navigation("TheCommentsList");

                    b.Navigation("TheFollowerList");

                    b.Navigation("TheFollowingList");

                    b.Navigation("ThePostLikedList");

                    b.Navigation("ThePostsList");

                    b.Navigation("TheRecievedMessagesList");

                    b.Navigation("TheSentMessagesList");

                    b.Navigation("TheUserPhotosList");
                });
#pragma warning restore 612, 618
        }
    }
}