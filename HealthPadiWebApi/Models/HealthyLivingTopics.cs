namespace HealthPadiWebApi.Models
{
    using System;
    using System.Collections.Generic;

    class HealthyLivingTopics
    {
        // List of 100 topics related to healthy living, wellness, exercises, and eating right.
        private static List<string> topics = new List<string>
    {
        "Benefits of Daily Walking",
        "Healthy Breakfast Ideas",
        "Importance of Hydration",
        "Stress Management Techniques",
        "Yoga for Beginners",
        "Meditation Practices",
        "Balanced Diet Essentials",
        "Healthy Snacks on the Go",
        "Importance of Sleep",
        "Cardio Workouts at Home",
        "Strength Training Basics",
        "Plant-Based Diet Benefits",
        "Mental Health Awareness",
        "Mindful Eating Tips",
        "Reducing Sugar Intake",
        "Benefits of Regular Exercise",
        "Intermittent Fasting Guide",
        "Healthy Meal Prep Tips",
        "Superfoods to Include in Your Diet",
        "High-Protein Diet Plans",
        "Low-Carb Diets Explained",
        "Healthy Cooking Techniques",
        "Reducing Salt in Your Diet",
        "Pilates for Core Strength",
        "Importance of Fiber",
        "Vitamins and Minerals Essentials",
        "Detox Diets: Myths and Facts",
        "Heart-Healthy Foods",
        "Staying Active at Work",
        "Healthy Aging Tips",
        "Managing Portion Sizes",
        "Best Foods for Skin Health",
        "Mind-Body Connection",
        "Boosting Your Immune System",
        "Healthy Eating on a Budget",
        "Benefits of Cycling",
        "Healthy Smoothie Recipes",
        "Understanding Food Labels",
        "Meal Planning for Busy People",
        "Healthy Weight Loss Tips",
        "Managing Food Cravings",
        "Exercises for Flexibility",
        "Gut Health and Probiotics",
        "The Role of Antioxidants",
        "Walking for Weight Loss",
        "Reducing Processed Foods",
        "Finding Time for Exercise",
        "Stress-Relief Exercises",
        "Understanding Macros",
        "Sleep Hygiene Practices",
        "Healthy Holiday Eating Tips",
        "Importance of Regular Check-ups",
        "Healthy Eating for Kids",
        "Functional Fitness Exercises",
        "Healthy Grilling Tips",
        "The Role of Omega-3 Fatty Acids",
        "Staying Motivated to Exercise",
        "Bone Health and Calcium",
        "Benefits of Outdoor Workouts",
        "Healthy Food Swaps",
        "Meal Replacement Shakes",
        "Importance of Stretching",
        "Sustainable Weight Management",
        "Tips for Reducing Anxiety",
        "Nutrient-Dense Foods",
        "Low-Sodium Diet Tips",
        "Post-Workout Nutrition",
        "Incorporating More Vegetables",
        "Healthy Desserts Options",
        "Benefits of Strength Training",
        "Healthy Fats to Include",
        "Importance of Whole Grains",
        "Building a Home Gym",
        "Importance of Regular Medical Check-ups",
        "Using Fitness Trackers",
        "Clean Eating Principles",
        "Healthy Eating Habits for the Whole Family",
        "Mindful Breathing Exercises",
        "Reducing Alcohol Intake",
        "Mental Benefits of Exercise",
        "Exercises for Better Posture",
        "Healthy Alternatives to Fast Food",
        "Reducing Food Waste",
        "Healthy Eating When Dining Out",
        "Effective Home Workouts",
        "Staying Hydrated in Hot Weather",
        "Vegetarian Diet Tips",
        "The Importance of Iron",
        "Meal Prep for Weight Loss",
        "Interval Training Benefits",
        "Healthy Eating for Athletes",
        "Boosting Energy Naturally",
        "Managing Blood Sugar Levels",
        "Reducing Inflammation Through Diet",
        "Staying Fit While Traveling",
        "Mindfulness Meditation for Stress Reduction",
        "Healthy Aging Through Nutrition",
        "Simple Exercises for Busy People",
        "Healthy Eating for Heart Health",
        "Benefits of Going Organic",
        "Healthy Eating for Mental Health",
        "Choosing the Right Supplements"
    };

        // Method to randomly select and return one of the topics, and remove it from the list
        public static string GetRandomTopic()
        {
            if (topics.Count == 0)
            {
                return "No more topics available.";
            }

            Random random = new Random();
            int index = random.Next(topics.Count);
            string selectedTopic = topics[index];

            // Remove the selected topic from the list
            topics.RemoveAt(index);

            return selectedTopic;
        }

    }

}
